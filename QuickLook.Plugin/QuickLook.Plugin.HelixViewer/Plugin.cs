﻿// Copyright © 2017-2025 QL-Win Contributors
//
// This file is part of QuickLook program.
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using QuickLook.Common.Plugin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace QuickLook.Plugin.HelixViewer;

public class Plugin : IViewer
{
    private static readonly HashSet<string> WellKnownExtensions = new(
    [
        ".stl", ".obj", ".3ds", ".lwo", ".ply",
        ".fbx",
    ]);

    private HelixPanel _hp;

    public int Priority => 0;

    public void Init()
    {
    }

    public bool CanHandle(string path)
    {
        return !Directory.Exists(path)
            && WellKnownExtensions.Contains(Path.GetExtension(path.ToLower()))
            && Handler.CanHandle(path);
    }

    public void Prepare(string path, ContextObject context)
    {
        context.PreferredSize = new Size { Width = 800, Height = 800 };
    }

    public void View(string path, ContextObject context)
    {
        _hp = new HelixPanel(path);
        context.ViewerContent = _hp;
        context.Title = Path.GetFileName(path);
        context.IsBusy = false;
    }

    public void Cleanup()
    {
        GC.SuppressFinalize(this);

        _hp = null;
    }
}
