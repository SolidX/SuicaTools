internal class FelicaCardIdentity
{
    private readonly byte[] _idm;
    private readonly byte[] _pmm;

    public FelicaCardIdentity(byte[] IDm, byte[] PMm)
    {
        if (IDm.Length != 8)
            throw new ArgumentOutOfRangeException(nameof(IDm), "Manufacturer ID must be 8 bytes in length.");
        if (PMm.Length != 8)
            throw new ArgumentOutOfRangeException(nameof(PMm), "Manufacturer Parameters must be 8 bytes in length.");

        _idm = IDm;
        _pmm = PMm;
    }

    public byte[] IDm { get { return _idm; } }
    public byte[] ManufacturerCode => new byte[] { _idm[0], _idm[1] };
    public byte[] CardIdNumber => _idm.TakeLast(6).ToArray();
    public byte[] PMm => _pmm;
    public byte RomType => _pmm[0];
    public byte IcType => _pmm[1];
    public byte[] IcCode => new byte[] { RomType, IcType };
    public byte[] MaximumResponseTime => _pmm.TakeLast(6).ToArray();
}