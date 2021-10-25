#pragma once
#include <Windows.h>
#include <winreg.h>

class RegistryManager {
public:
	BOOL CreateRegistry(HKEY hKey, LPCWSTR hSubKey, PHKEY hResult);
	BOOL DeleteRegistry(HKEY hKey, LPCWSTR hSubKey);
};
