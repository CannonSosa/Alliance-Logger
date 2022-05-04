using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api
{
  [Serializable]
  public class EncryptionKeys
  {
    public byte[] AesKey { get; set; }

    public byte[] AesIv { get; set; }
  }
}
