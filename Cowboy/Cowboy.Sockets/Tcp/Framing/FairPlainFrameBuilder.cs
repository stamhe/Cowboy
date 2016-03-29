﻿using System;

namespace Cowboy.Sockets
{
    public sealed class FairPlainFrameBuilder : IFrameBuilder
    {
        public FairPlainFrameBuilder()
            : this(new FairPlainFrameEncoder(), new FairPlainFrameDecoder())
        {
        }

        public FairPlainFrameBuilder(IFrameEncoder encoder, IFrameDecoder decoder)
        {
            if (encoder == null)
                throw new ArgumentNullException("encoder");
            if (decoder == null)
                throw new ArgumentNullException("decoder");

            this.Encoder = encoder;
            this.Decoder = decoder;
        }

        public IFrameEncoder Encoder { get; private set; }
        public IFrameDecoder Decoder { get; private set; }
    }

    public sealed class FairPlainFrameEncoder : AbstractChainableFrameEncoder
    {
        public FairPlainFrameEncoder()
        {
        }

        protected override void OnEncodeFrame(byte[] payload, int offset, int count, out byte[] frameBuffer, out int frameBufferOffset, out int frameBufferLength)
        {
            frameBuffer = payload;
            frameBufferOffset = offset;
            frameBufferLength = count;
        }
    }

    public sealed class FairPlainFrameDecoder : AbstractChainableFrameDecoder
    {
        public FairPlainFrameDecoder()
        {
        }

        protected override bool OnTryDecodeFrame(byte[] buffer, int offset, int count, out int frameLength, out byte[] payload, out int payloadOffset, out int payloadCount)
        {
            frameLength = 0;
            payload = null;
            payloadOffset = 0;
            payloadCount = 0;

            if (count <= 0)
                return false;

            frameLength = count;
            payload = buffer;
            payloadOffset = offset;
            payloadCount = count;
            return true;
        }
    }
}