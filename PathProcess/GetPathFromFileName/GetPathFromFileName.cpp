// GetPathFromFileName.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <Windows.h>
#include <stdio.h>
#include <pathcch.h>
#include <strsafe.h>
using namespace std;

#define TOGGLE_GLANCE L"\\ToggleGlance.exe"

LPWSTR ConvertString(const std::string& instr)
{
    // Assumes std::string is encoded in the current Windows ANSI codepage
    int bufferlen = ::MultiByteToWideChar(CP_ACP, 0, instr.c_str(), instr.size(), NULL, 0);

    if (bufferlen == 0)
    {
        // Something went wrong. Perhaps, check GetLastError() and log.
        return 0;
    }

    // Allocate new LPWSTR - must deallocate it later
    LPWSTR widestr = new WCHAR[bufferlen + 1];

    ::MultiByteToWideChar(CP_ACP, 0, instr.c_str(), instr.size(), widestr, bufferlen);

    // Ensure wide string is null terminated
    widestr[bufferlen] = 0;

    // Do something with widestr
    return widestr;
    //delete[] widestr;
}

std::wstring s2ws(const std::string& s)
{
    int len;
    int slength = (int)s.length() + 1;
    len = MultiByteToWideChar(CP_ACP, 0, s.c_str(), slength, 0, 0);
    wchar_t* buf = new wchar_t[len];
    MultiByteToWideChar(CP_ACP, 0, s.c_str(), slength, buf, len);
    std::wstring r(buf);
    delete[] buf;
    return r;
}

int main()
{
    //TCHAR strPath[] = TEXT("C:\\hello\\world.exe");
    WCHAR strTemp[MAX_PATH] = TEXT("C:\\hello\\world.exe");
    LPWSTR strPath = new WCHAR[MAX_PATH];
    strPath = strTemp;
    DWORD strLen = lstrlen(strTemp);

    wcout << strPath << endl;
    PathCchRemoveFileSpec(strPath, strLen);
    wcout << strPath << endl;

    StringCchCatW(strPath, MAX_PATH, TOGGLE_GLANCE);
    wcout << strPath << endl;
    //TChar to LPWSTR
    //s2ws(const_cast<>strPath);
    return 0;
}



// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file