using System;
using Messaging;

namespace Core.PubSub
{
    public class BufferedMessageChannel<T> : MessageChannel<T>, IBufferedMessageChannel<T> where T : IMessage
    {
        public override void Publish(T message)
        {
            HasBufferedMessage = true;
            BufferedMessage = message;
            base.Publish(message);
        }

        public override IDisposable Subscribe(Action<T> handler)
        {
            var subscription = base.Subscribe(handler);

            if (HasBufferedMessage) handler?.Invoke(BufferedMessage);

            return subscription;
        }

        public bool HasBufferedMessage { get; private set; } = false;
        public T BufferedMessage { get; private set; }
    }
}