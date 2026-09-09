# AOT

> A lightweight Windows desktop viewer for `.aot` plain-text notes with live rich-text formatting.

[![.NET Framework](https://img.shields.io/badge/.NET_Framework-4.7.2-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net472)
[![Platform](https://img.shields.io/badge/platform-Windows-0078D6?logo=windows&logoColor=white)](https://github.com/Moixia/AOT)
[![Language](https://img.shields.io/badge/language-C%23-239120?logo=csharp&logoColor=white)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![UI](https://img.shields.io/badge/UI-WinForms-68217A)](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/)
[![License](https://img.shields.io/badge/license-TBD-lightgrey)](./LICENSE)

## Table of Contents

- [About](#about)
- [Features](#features)
- [Markup Syntax](#markup-syntax)
- [Screenshots](#screenshots)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Clone](#clone)
  - [Build](#build)
  - [Run](#run)
- [Usage](#usage)
- [Project Structure](#project-structure)
- [How It Works](#how-it-works)
- [Configuration](#configuration)
- [Tech Stack](#tech-stack)
- [Roadmap](#roadmap)
- [Contributing](#contributing)
- [License](#license)

## About

AOT is a Windows Forms desktop application for browsing and reading `.aot` files stored as plain text on disk. Point it at a root folder, navigate the folder tree, and read each note rendered with lightweight inline markup (bold, italic, tags, and links) in a flicker-free `RichTextBox`.

The project is in an early, single-form stage: one solution, one project, one main window (`Form1`). There is no database, no network layer, and no installer — files are read directly from the file system.

Repository: <https://github.com/Moixia/AOT>

## Features

- **Folder-scoped note browser** — type a root path and browse its directory tree; only `*.aot` files are listed.
- **One-click preview** — select a file to load its full text; select a folder to list the `.aot` files it directly contains.
- **Live lightweight markup** — formatting refreshes on every edit without losing caret or scroll position:
  - `*bold*` → **bold**
  - `_italic_` → *italic* (boundary-aware, so `snake_case` identifiers are left alone)
  - `[Tag]` → green bold label
  - `@reference` and `http(s)://...` → blue clickable link
- **Flicker-free rendering** — redraw is suspended with `WM_SETREDRAW` and scroll position is preserved with `EM_GETSCROLLPOS` / `EM_SETSCROLLPOS` while styles are reapplied.
- **DPI-aware startup** — calls `SetProcessDPIAware()` on Windows Vista and later before launching the main form.
- **Zero dependencies** — only .NET Framework BCL assemblies (`System.Windows.Forms`, `System.Drawing`, `System.Text.RegularExpressions`, P/Invoke to `user32.dll`).

## Markup Syntax

| Syntax | Example input | Rendered as |
| --- | --- | --- |
| Bold | `*important*` | **important** (bold, body color) |
| Italic | `_emphasis_` | *emphasis* (italic, body color) |
| Tag | `[meeting-notes]` | green, bold, non-clickable |
| Reference | `@C:\notes\todo.aot` | blue, clickable link |
| URL | `https://example.com` | blue, clickable link |

Rules worth knowing:

- Bold and italic markers only trigger when surrounded by whitespace or line boundaries: `*bold*` works, `a*b*c` and `aot_file` do not.
- Links and tags are applied **after** bold/italic and override them, so `[@link]` renders as a tag, not as bold.
- URL auto-detection by the `RichTextBox` itself is disabled (`DetectUrls = false`); link styling is fully controlled by the app's regex pass plus `AutoUrlDetect` (`EM_AUTOURLDETECT`).

Example `.aot` file:

```text
[project-kickoff] @C:\notes\attendees.aot

Welcome to the *AOT* reader. Please read _carefully_ before editing.

Useful links:
https://github.com/Moixia/AOT
@C:\notes\follow-ups.aot
```

## Screenshots

> TODO: add a screenshot of the main window (`docs/screenshot-main.png`). Suggested capture: left tree with a folder of `.aot` files selected, right pane showing formatted bold/italic/tags/links.

```text
docs/
  screenshot-main.png   # <-- add here, then embed with:
  # ![AOT main window](docs/screenshot-main.png)
```

## Getting Started

### Prerequisites

| Requirement | Version / notes |
| --- | --- |
| OS | Windows 10/11 (Win32 P/Invoke to `user32.dll`) |
| .NET Framework | 4.7.2 (target) + Developer Pack for building |
| IDE | Visual Studio 2022 (or VS 2019 with `.slnx` support) / MSBuild 15+ |
| Git | Any recent version |

### Clone

```bash
git clone https://github.com/Moixia/AOT.git
cd AOT
```

### Build

Using Visual Studio:

1. Open `AOT.slnx`.
2. Select configuration `Debug` (or `Release`) + `AnyCPU`.
3. **Build → Build Solution** (or <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>B</kbd>).

Using MSBuild from a Developer Command Prompt:

```powershell
msbuild AOT.csproj /p:Configuration=Release /p:Platform=AnyCPU
```

Output lands in `bin\Debug\` or `bin\Release\` (`AOT.exe`).

### Run

- From Visual Studio: press <kbd>F5</kbd> (Debug) or <kbd>Ctrl</kbd>+<kbd>F5</kbd> (without debugging).
- From disk: run `bin\Debug\AOT.exe` (or `bin\Release\AOT.exe`).

No installation, no arguments, no config file values to set — the app starts with a default root folder (see [Configuration](#configuration)).

## Usage

1. Launch the app. The top-left text box shows the current root folder.
2. Edit the root path (e.g. `C:\Users\you\Desktop\Notes`) and press <kbd>Enter</kbd>/<kbd>Tab</kbd> or move focus — the tree rebuilds from that directory.
3. In the left `TreeView`:
   - Select a **folder** → the right pane lists that folder's top-level `*.aot` files as `[name] @full-path` lines.
   - Select a **file** → its full text loads into the right editor with markup applied; the top-right box shows its full path.
4. Edit text directly in the right pane — formatting updates live. Bold/italic/tags/links re-render on each change.
5. The two `/help` boxes at the bottom are currently placeholders and have no behavior.

> [!NOTE]
> Only files with the `.aot` extension (case-insensitive) appear in the tree. Subdirectories are included recursively; per-folder listings are top-level only (`SearchOption.TopDirectoryOnly`).

## Project Structure

```text
AOT/
├── AOT.slnx                  # Solution (new XML solution format)
├── AOT.csproj                # WinExe project, .NET Framework 4.7.2, AnyCPU
├── Program.cs                # Entry point: DPI awareness + Application.Run(new Form1())
├── Form1.cs                  # All behavior: tree fill, file loading, regex formatting, Win32 interop
├── Form1.Designer.cs         # SplitContainer layout: TreeView/TextBoxes | RichTextBox/TextBoxes
├── Form1.resx                # Default RichTextBox sample text (Lorem Ipsum demo content)
├── App.config                # Supported runtime: .NET Framework 4.7.2
└── Properties/
    ├── AssemblyInfo.cs       # Assembly metadata (title, version 1.0.0.0, GUID)
    ├── Resources.resx        # Project resources
    └── Settings.settings     # User settings store
```

Key files:

- [`Program.cs`](./Program.cs) — `Main()` with `[STAThread]`, visual styles, and `SetProcessDPIAware()`.
- [`Form1.cs`](./Form1.cs) — `FillNode()`, `RefreshFormatting()`, `ApplyStyle()`, `SetLink()` (`CHARFORMAT2` via `EM_SETCHARFORMAT`).
- [`Form1.Designer.cs`](./Form1.Designer.cs) — control tree and docking (`splitContainer1`, `FileSystemTreeView`, `RichTextBoxContent`, path boxes).

## How It Works

The rendering pipeline in `Form1.RefreshFormatting()` runs on every text change:

1. **Snapshot state** — caret position and scroll offset (`EM_GETSCROLLPOS`), then suspend painting (`WM_SETREDRAW`, `wParam = 0`).
2. **Reset** — select all, clear link flags (`CFM_LINK` via `CHARFORMAT2`), restore default color and regular font.
3. **Inline styles** — apply bold for `(?<=^|\s)\*(.*?)\*(?=$|\s)` and italic for the `_..._` equivalent. The lookbehind/lookahead guards prevent `snake_case` false positives.
4. **Links and tags (highest priority)** — regex `(@[^\n\r]+|https?://\S+)` → blue + `CFE_LINK`; `\[(.*?)\]` → `MediumSeaGreen` + bold, link flag cleared.
5. **Restore** — reselect the original range, restore scroll (`EM_SETSCROLLPOS`), resume painting (`WM_SETREDRAW`, `wParam = 1`), `Invalidate()`.

A re-entrancy guard (`_upd`) prevents the `TextChanged` handler from recursing while styles are applied.

Win32 messages used (all via `SendMessage` to the `RichTextBox` handle):

| Message | Value | Purpose |
| --- | --- | --- |
| `EM_AUTOURLDETECT` | `0x0400 + 77` | Enable automatic URL detection (`wParam = 0x02`) |
| `EM_SETCHARFORMAT` | `0x0400 + 68` | Apply `CHARFORMAT2` (link on/off) |
| `WM_SETREDRAW` | `0x000B` | Freeze/thaw painting during restyle |
| `EM_GETSCROLLPOS` | `0x0400 + 221` | Save scroll position |
| `EM_SETSCROLLPOS` | `0x0400 + 222` | Restore scroll position |

## Configuration

| Setting | Location | Default | Notes |
| --- | --- | --- | --- |
| `StartDir` | `Form1.cs` (`public string StartDir`) | `C:\Users\modt\Desktop\Playground` | Initial root folder shown on launch; overwritten by the path box at runtime. Change the initializer to set your own default. |

There are no user settings, environment variables, or `App.config` app-settings at this stage.

## Tech Stack

- **Language:** C# (classic `.csproj`, `ToolsVersion 15.0`)
- **Runtime:** .NET Framework 4.7.2
- **UI:** Windows Forms (`SplitContainer`, `TreeView`, `RichTextBox`, `TextBox`)
- **Interop:** P/Invoke to `user32.dll` (`SendMessage`, `SetProcessDPIAware`)
- **Parsing:** `System.Text.RegularExpressions` (five patterns, applied in priority order)

## Roadmap

- [ ] Rename window title from `Form1` and give path/command boxes meaningful names
- [ ] Persist last root folder between sessions (user settings)
- [ ] Save edits back to disk (currently load-only in practice) with dirty-state indicator
- [ ] Lazy-load tree nodes (currently `ExpandAll()` on every path change)
- [ ] Search across `.aot` files and jump-to-line
- [ ] Wire up the `/help` command boxes or remove them
- [ ] Add `LICENSE`, screenshots under `docs/`, and CI build validation

## Contributing

Contributions are welcome. Basic flow:

1. Fork the repo and create a feature branch: `git checkout -b feat/my-change`.
2. Build in `Release / AnyCPU` and verify the app launches and formats a sample `.aot` file.
3. Keep changes scoped to the WinForms/.NET Framework 4.7.2 baseline (no new runtime dependencies without discussion).
4. Open a pull request describing what changed and how you tested it.

No issue template, changelog, or code-of-conduct files exist yet — a clear PR description is enough for now.

## License

No license has been declared for this repository yet (there is no `LICENSE` file and `AssemblyDescription`/`AssemblyCompany` are empty). Until one is added, all rights are reserved by the author by default. If you intend to use or distribute this code, open an issue asking the maintainer to choose a license (MIT is a common choice for projects like this).
