using System;
using Messaging;

namespace Core.PubSub
{
    public interface IPublisher<T> where T : IMessage
    {
        void Publish(T message);
    }

    public interface ISubscriber<T> where T : IMessage
    {
        IDisposable Subscribe(Action<T> handler);
    }

    public interface IMessageChannel<T> : IPublisher<T>, ISubscriber<T>, IDisposable where T : IMessage
    {
        bool IsDisposed { get; }
        void Unsubscribe(Action<T> handler);
    }

    public interface IBufferedMessageChannel<T> : IMessageChannel<T> where T : IMessage
    {
        bool HasBufferedMessage { get; }
        T BufferedMessage { get; }
    }
}