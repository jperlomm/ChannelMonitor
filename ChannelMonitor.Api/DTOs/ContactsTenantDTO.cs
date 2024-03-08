﻿using ChannelMonitor.Api.Entities;

namespace ChannelMonitor.Api.DTOs
{
    public class ContactsTenantDTO
    {
        public int MessageProviderId { get; set; }
        public MessageProvider MessageProvider { get; set; } = null!;
        public string Number { get; set; } = null!;
    }
}
