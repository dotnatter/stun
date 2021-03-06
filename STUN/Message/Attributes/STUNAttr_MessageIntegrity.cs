using STUN.Crypto;
using STUN.Message.Enums;
using STUN.NetBuffer;
using System.Text;

namespace STUN.Message.Attributes {
	public struct STUNAttr_MessageIntegrity : ISTUNAttr {
		public const STUNAttribute TYPE = STUNAttribute.MESSAGE_INTEGRITY;
		private const ushort HMAC_LENGTH = 20;

		private HMAC_SHA1 hmacsha1Instance;

		public STUNAttr_MessageIntegrity(string key, ref HMAC_SHA1 hmacsha1Instance) {
			if (null == hmacsha1Instance)
				hmacsha1Instance = new HMAC_SHA1(Encoding.UTF8.GetBytes(key));
			this.hmacsha1Instance = hmacsha1Instance;
		}

		public void WriteToBuffer(ref ByteBuffer buffer) {
			int count = buffer.Position;
			STUNTypeLengthValue.WriteTypeLength(TYPE, HMAC_LENGTH, ref buffer);
			STUNMessageBuilder.UpdateHeaderAttributesLength(ref buffer, buffer.Position + HMAC_LENGTH);
			hmacsha1Instance.ComputeHash(buffer.data, buffer.absOffset, count, buffer.data, buffer.absPosition);
			buffer.Position += HMAC_LENGTH;
			STUNTypeLengthValue.AddPadding(ref buffer);
		}

		public void ReadFromBuffer(STUNAttr attr) {
			throw new System.NotImplementedException();
		}
	}
}
