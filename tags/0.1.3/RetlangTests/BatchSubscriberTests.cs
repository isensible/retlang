using System;
using System.Collections.Generic;
using System.Text;
using Rhino.Mocks;
using NUnit.Framework;
using Retlang;

namespace RetlangTests
{

    [TestFixture]
    public class BatchSubscriberTests
    {

        [Test]
        public void Batch()
        {
            MockRepository repo = new MockRepository();
            IProcessContext context = repo.CreateMock<IProcessContext>();
            On<IList<IMessageEnvelope<object>>> callback = repo.CreateMock<On<IList<IMessageEnvelope<object>>>>();
            callback(null);
            LastCall.IgnoreArguments();
            IMessageHeader header = repo.CreateMock<IMessageHeader>();

            BatchSubscriber<object> batch = new BatchSubscriber<object>(callback, context, 0);

            context.Enqueue(batch.Flush);

            repo.ReplayAll();

            batch.ReceiveMessage(header, new object());
            batch.Flush();
        }

        [Test]
        public void BatchWithInterval()
        {
            MockRepository repo = new MockRepository();
            IProcessContext context = repo.CreateMock<IProcessContext>();
            On<IList<IMessageEnvelope<object>>> callback = repo.CreateMock<On<IList<IMessageEnvelope<object>>>>();
            callback(null);
            LastCall.IgnoreArguments();
            IMessageHeader header = repo.CreateMock<IMessageHeader>();

            BatchSubscriber<object> batch = new BatchSubscriber<object>(callback, context, 100);

            context.Schedule(batch.Flush, 100);

            repo.ReplayAll();

            batch.ReceiveMessage(header, new object());
            batch.Flush();

        }

    }
}
