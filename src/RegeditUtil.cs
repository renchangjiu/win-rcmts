﻿using Microsoft.Win32;

namespace win_rcmts;

public static class RegeditUtil {

    public static void add(MenuType type, string menuName, string iconPath, string command) {
        RegistryKey shellKey = getShellKey(type);

        RegistryKey menuKey = shellKey.CreateSubKey(menuName, true);
        menuKey.SetValue("icon", iconPath, RegistryValueKind.String);
        RegistryKey cmdKey = menuKey.CreateSubKey("command", true);
        cmdKey.SetValue("", command, RegistryValueKind.String);
        menuKey.Close();
    }

    public static void remove(MenuType type, string menuName) {
        RegistryKey shellKey = getShellKey(type);
        shellKey.DeleteSubKeyTree(menuName);
        shellKey.Close();
    }

    private static RegistryKey getShellKey(MenuType type) {
        RegistryKey root = Registry.ClassesRoot;
        return root.OpenSubKey(type.regeditPath, true);
    }

}