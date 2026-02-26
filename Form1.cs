using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AOT
{
    public partial class Form1 : Form
    {
        public string StartDir { get; set; } = @"C:\Users\modt\Desktop\Playground";

        [StructLayout(LayoutKind.Sequential)] struct POINT { public int X, Y; }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        struct CF2
        {
            public int cb; public uint mk, ef; public int h, o, tc; public byte cs, pf;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] public string fn;
            public short w, s; public int bc, lcid, ck; public short k; public byte ut, an, ra;
        }

        [DllImport("user32.dll")] static extern IntPtr SendMessage(IntPtr h, int m, IntPtr w, ref CF2 l);
        [DllImport("user32.dll")] static extern IntPtr SendMessage(IntPtr h, int m, IntPtr w, IntPtr l);
        [DllImport("user32.dll")] static extern IntPtr SendMessage(IntPtr h, int m, IntPtr w, ref POINT l);

        private bool _upd = false;

        public Form1()
        {
            InitializeComponent();
            RichTextBoxContent.DetectUrls = false;
            SendMessage(RichTextBoxContent.Handle, 0x0400 + 77, (IntPtr)0x02, (IntPtr)0);

            Load += (s, e) => MainFolderDirTextBox.Text = StartDir;

            MainFolderDirTextBox.TextChanged += (s, e) => {
                FileSystemTreeView.Nodes.Clear();
                if (Directory.Exists(MainFolderDirTextBox.Text))
                {
                    FileSystemTreeView.Nodes.Add(FillNode(new DirectoryInfo(MainFolderDirTextBox.Text)));
                    FileSystemTreeView.ExpandAll();
                }
            };

            FileSystemTreeView.AfterSelect += (s, e) => {
                string path = e.Node.Tag?.ToString();
                if (File.Exists(path))
                {
                    ActualFileDirTextBox.Text = path;
                    RichTextBoxContent.Text = File.ReadAllText(path);
                }
                else if (Directory.Exists(path ?? e.Node.FullPath))
                {
                    ActualFileDirTextBox.Text = "";
                    var files = Directory.GetFiles(path ?? e.Node.FullPath, "*.aot", SearchOption.TopDirectoryOnly)
                                         .Select(f => $"[{Path.GetFileName(f)}] @{f}");
                    RichTextBoxContent.Text = string.Join(Environment.NewLine, files);
                }
                RefreshFormatting();
            };

            RichTextBoxContent.TextChanged += (s, e) => { if (!_upd) RefreshFormatting(); };
        }

        private void RefreshFormatting()
        {
            if (string.IsNullOrEmpty(RichTextBoxContent.Text)) return;

            _upd = true;
            POINT scroll = new POINT();
            SendMessage(RichTextBoxContent.Handle, 0x0400 + 221, IntPtr.Zero, ref scroll);
            int start = RichTextBoxContent.SelectionStart, len = RichTextBoxContent.SelectionLength;

            SendMessage(RichTextBoxContent.Handle, 0x000B, IntPtr.Zero, IntPtr.Zero);

            try
            {
                // 1. RESET TOTAL
                RichTextBoxContent.SelectAll();
                SetLink(false);
                RichTextBoxContent.SelectionColor = RichTextBoxContent.ForeColor;
                RichTextBoxContent.SelectionFont = new Font(RichTextBoxContent.Font, FontStyle.Regular);

                // 2. APLICAR CURSIVAS Y NEGRITAS PRIMERO (con precaución)
                // Solo aplica si el asterisco/guion bajo está rodeado de espacios o al inicio/fin de línea
                // Esto evita que "aot_file" se convierta en cursiva.
                ApplyStyle(@"(?<=^|\s)\*(.*?)\*(?=$|\s)", FontStyle.Bold);
                ApplyStyle(@"(?<=^|\s)_(.*?)_(?=$|\s)", FontStyle.Italic);

                // 3. SOBREESCRIBIR CON ENLACES Y ETIQUETAS (Prioridad alta)
                // Al hacerlo después, los enlaces limpian cualquier cursiva accidental

                // Enlaces (@ y http)
                foreach (Match m in Regex.Matches(RichTextBoxContent.Text, @"(@[^\n\r]+|https?://\S+)"))
                {
                    RichTextBoxContent.Select(m.Index, m.Length);
                    SetLink(true);
                    RichTextBoxContent.SelectionColor = Color.CornflowerBlue;
                    // Forzamos que los links no tengan cursiva/negrita heredada
                    RichTextBoxContent.SelectionFont = new Font(RichTextBoxContent.Font, FontStyle.Regular);
                }

                // Etiquetas [Texto]
                foreach (Match m in Regex.Matches(RichTextBoxContent.Text, @"\[(.*?)\]"))
                {
                    RichTextBoxContent.Select(m.Index, m.Length);
                    SetLink(false); // Los corchetes no son links
                    RichTextBoxContent.SelectionColor = Color.MediumSeaGreen;
                    RichTextBoxContent.SelectionFont = new Font(RichTextBoxContent.Font, FontStyle.Bold);
                }

                RichTextBoxContent.Select(start, len);
                SendMessage(RichTextBoxContent.Handle, 0x0400 + 222, IntPtr.Zero, ref scroll);
            }
            finally
            {
                SendMessage(RichTextBoxContent.Handle, 0x000B, (IntPtr)1, IntPtr.Zero);
                RichTextBoxContent.Invalidate();
                _upd = false;
            }
        }

        private void ApplyStyle(string pattern, FontStyle style)
        {
            foreach (Match m in Regex.Matches(RichTextBoxContent.Text, pattern))
            {
                RichTextBoxContent.Select(m.Index, m.Length);
                RichTextBoxContent.SelectionFont = new Font(RichTextBoxContent.Font, style);
            }
        }

        private void SetLink(bool add)
        {
            var cf = new CF2 { cb = Marshal.SizeOf(typeof(CF2)), mk = 0x20, ef = add ? 0x20u : 0 };
            SendMessage(RichTextBoxContent.Handle, 0x0400 + 68, (IntPtr)1, ref cf);
        }

        private TreeNode FillNode(DirectoryInfo d) =>
            new TreeNode(d.Name, d.GetDirectories().Select(FillNode)
                .Concat(d.GetFiles().Where(f => f.Extension.ToLower() == ".aot")
                .Select(f => new TreeNode(f.Name) { Tag = f.FullName }))
                .ToArray())
            { Tag = d.FullName };
    }
}