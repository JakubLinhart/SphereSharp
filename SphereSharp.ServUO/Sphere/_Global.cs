using System;

namespace SphereSharp.ServUO.Sphere
{
    public static partial class _Global
    {
        public static object NULL => null;
        public static HRESULT NO_ERROR = 0;

        public static HRESULT HRES_INVALID_INDEX = new HRESULT();
        public static HRESULT HRES_INVALID_HANDLE = new HRESULT();
        public static void ASSERT(object obj) { }
        public static void DEBUG_CHECK(object obj) { }

        public static CSphereUID UID_INDEX_CLEAR = new CSphereUID(0);

        public static T REF_CAST<T>(object obj) => (T)obj;
    }

    public struct HRESULT
    {
        public int Value { get; }

        public HRESULT(int val)
        {
            Value = val;
        }

        public static implicit operator HRESULT(int val) => new HRESULT(val);
    }

    public struct WORD
    {
        public ushort Value { get; }

        public WORD(ushort val)
        {
            Value = val;
        }

        public static implicit operator WORD(ushort val) => new WORD(val);
        public static implicit operator WORD(int val) => new WORD((ushort)val);
        public static implicit operator int(WORD val) => val.Value;
        public static implicit operator bool(WORD val) => val.Value != 0;

        public static WORD operator &(WORD val1, WORD val2) => val1.Value & val2.Value;
    }

    public struct SOUND_TYPE
    {
        public ushort Value { get; }

        public SOUND_TYPE(ushort val)
        {
            Value = val;
        }

        public static implicit operator SOUND_TYPE(ushort val) => new SOUND_TYPE(val);
        public static implicit operator SOUND_TYPE(int val) => new SOUND_TYPE((ushort)val);
        public static implicit operator int(SOUND_TYPE val) => val.Value;
        public static implicit operator bool(SOUND_TYPE val) => val.Value != 0;

        public static SOUND_TYPE operator &(SOUND_TYPE val1, SOUND_TYPE val2) => val1.Value & val2.Value;
    }

    public struct DAMAGE_TYPE
    {
        public ushort Value { get; }

        public DAMAGE_TYPE(ushort val)
        {
            Value = val;
        }

        public static implicit operator DAMAGE_TYPE(ushort val) => new DAMAGE_TYPE(val);
        public static implicit operator DAMAGE_TYPE(int val) => new DAMAGE_TYPE((ushort)val);
        public static implicit operator int(DAMAGE_TYPE val) => val.Value;
        public static implicit operator bool(DAMAGE_TYPE val) => val.Value != 0;

        public static DAMAGE_TYPE operator &(DAMAGE_TYPE val1, DAMAGE_TYPE val2) => val1.Value & val2.Value;
        public static DAMAGE_TYPE operator |(DAMAGE_TYPE val1, DAMAGE_TYPE val2) => val1.Value | val2.Value;
    }

    public struct BYTE
    {
        public ushort Value { get; }

        public BYTE(byte val)
        {
            Value = val;
        }

        public static implicit operator BYTE(byte val) => new BYTE(val);
        public static implicit operator BYTE(int val) => new BYTE((byte)val);
        public static implicit operator int(BYTE val) => val.Value;
        public static implicit operator bool(BYTE val) => val.Value != 0;

        public static BYTE operator &(BYTE val1, BYTE val2) => val1.Value & val2.Value;
    }

    public struct DWORD
    {
        public uint Value { get; }

        public DWORD(uint val)
        {
            Value = val;
        }

        public static implicit operator DWORD(uint val) => new DWORD(val);
        public static implicit operator DWORD(int val) => new DWORD((uint)val);
        public static implicit operator int(DWORD val) => (int)val.Value;
        public static implicit operator bool(DWORD val) => val.Value != 0;

        public static DWORD operator &(DWORD val1, DWORD val2) => val1.Value & val2.Value;
    }
}
