using System;
using System.Collections.Generic;

namespace MessagingSystem
{
    public static class MessageSystem
    {
        public interface IMessageEnvelope
        {
            Type MessageType { get; }
        }

        private interface IMessageEnvelope<T> : IMessageEnvelope
        {
            T Message { get; }
        }

        public static T Message<T>(this IMessageEnvelope envelope)
        {
            IMessageEnvelope<T> e = envelope as IMessageEnvelope<T>;
            return e.Message;
        }

        public static class MessageManager
        {
            private class MessageEnvelope<T> : IMessageEnvelope, IMessageEnvelope<T> 
            {
                public T Message { get; private set; }
                public Type MessageType { get; private set; } = typeof(T);

                public MessageEnvelope(T message)
                {
                    Message = message;
                }
            }

            private static Dictionary<string, Action<IMessageEnvelope>> _channels = new Dictionary<string, Action<IMessageEnvelope>>();

            /// <summary>
            /// Sends a message to a given channel
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="channel">Channel</param>
            /// <param name="message">Message</param>
            public static void Send<T>(string channel, T message)
            {
                //TODO: Allocate the message envelope with allocation pool
                MessageEnvelope<T> envelope = new MessageEnvelope<T>(message);

                if(_channels.ContainsKey(channel) && 
                    _channels[channel] != null)
                {
                    _channels[channel](envelope);
                }
            }

            /// <summary>
            /// Registers an action method to a given channel
            /// </summary>
            /// <param name="channel">Channel</param>
            /// <param name="handler">Handling function</param>
            public static void RegisterForChannel(string channel, Action<IMessageEnvelope> handler)
            {
                if(!_channels.ContainsKey(channel))
                {
                    _channels[channel] = handler;
                    return;
                }

                _channels[channel] += handler;
            }

            /// <summary>
            /// Registers an action method to multiple channels
            /// </summary>
            /// <param name="channels">Array of channels</param>
            /// <param name="handler">Handling function</param>
            public static void RegisterForChannel(string[] channels, Action<IMessageEnvelope> handler)
            {
                int length = channels.Length;
                for(int i=0;i<length;++i)
                {
                    RegisterForChannel(channels[i], handler);
                }
            }

            /// <summary>
            /// Unregisters an action method from a channel
            /// </summary>
            /// <param name="channel">Channel</param>
            /// <param name="handler">Handling function</param>
            public static void UnregisterForChannel(string channel, Action<IMessageEnvelope> handler)
            {
                if(!_channels.ContainsKey(channel))
                {
                    return;
                }
                _channels[channel] -= handler;
            }

            /// <summary>
            /// Unregisters an action method from a group of channels
            /// </summary>
            /// <param name="channels">Array of channels</param>
            /// <param name="handler">Handling function</param>
            public static void UnregisterForChannel(string[] channels, Action<IMessageEnvelope> handler)
            {
                int length = channels.Length;
                for(int i=0;i<length;++i)
                {
                    UnregisterForChannel(channels[i], handler);
                }
            }
        }
    }
}
