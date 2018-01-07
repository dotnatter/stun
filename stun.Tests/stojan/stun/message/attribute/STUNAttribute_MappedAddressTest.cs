using NUnit.Framework;

namespace STUN.me.stojan.stun.message.attribute {
	[TestFixture]
	public class STUNAttribute_MappedAddressTest {
		[Test]
		public void FullTestIPv4() {
			byte[] expected = new byte[] {
				0x01, 0x01, 0x00, 0x0C, 0x21, 0x12, 0xA4, 0x42,
				0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
				0x08, 0x09, 0x0A, 0x0B, 0x00, 0x01, 0x00, 0x08,
				0x00, 0x01, 0x12, 0x34, 0x01, 0x02, 0x03, 0x04
			};

			var msg = new STUNMessageBuilder(new byte[128]);
			msg.SetMessageType(STUNClass.Success, STUNMethod.Binding);
			msg.SetTransaction(new Transaction(new byte[12] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }));
			msg.WriteAttribute(new Attr_MappedAddress(System.Net.IPAddress.Parse("1.2.3.4"), 4660));
			var stunReq = msg.Build();

			CollectionAssert.AreEqual(expected, stunReq.ToArray());
		}

		[Test]
		public void FullTestIPv6() {
			// WRONG: regenerate
			byte[] expected = new byte[] {
				0x01, 0x01, 0x00, 0x18, 0x21, 0x12, 0xA4, 0x42,
				0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
				0x08, 0x09, 0x0A, 0x0B, 0x00, 0x01, 0x00, 0x14,
				0x00, 0x02, 0x12, 0x34, 0x20, 0x01, 0x0D, 0xB8,
				0x85, 0xA3, 0x00, 0x00, 0x00, 0x00, 0x8A, 0x2E,
				0x03, 0x70, 0x73, 0x34
			};

			var msg = new STUNMessageBuilder(new byte[128]);
			msg.SetMessageType(STUNClass.Success, STUNMethod.Binding);
			msg.SetTransaction(new Transaction(new byte[12] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }));
			msg.WriteAttribute(new Attr_MappedAddress(System.Net.IPAddress.Parse("[2001:0db8:85a3:0000:0000:8a2e:0370:7334]"), 4660));
			var stunReq = msg.Build();

			CollectionAssert.AreEqual(expected, stunReq.ToArray());
		}
	}
}