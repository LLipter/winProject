// CppLibrary.cpp: 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include "CppLibrary.h"


int __stdcall getMax(int a, int b) {
	return a > b ? a : b;
}

int __stdcall getMin(int a, int b) {
	return a < b ? a : b;
}