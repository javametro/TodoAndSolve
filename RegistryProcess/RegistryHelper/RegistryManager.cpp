#include "Header.h"

BOOL RegistryManager::CreateRegistry(HKEY hKey, LPCWSTR hSubKey, PHKEY hResult) {
	BOOL bRet = FALSE;
	LSTATUS lStatus = RegCreateKeyEx(hKey, hSubKey, 0, NULL, 0, KEY_ALL_ACCESS, NULL,
		hResult, NULL);
	if (lStatus == ERROR_SUCCESS) {
		bRet = TRUE;
		MessageBox(NULL, L"Create Key Success", L"RegistryTest", NULL);
	}
	else {
		bRet = FALSE;
		MessageBox(NULL, L"Create Key Failed", L"RegistryTest", NULL);
	}

	return bRet;
}

BOOL RegistryManager::DeleteRegistry(HKEY hKey, LPCWSTR hSubKey) {
	return FALSE;
}