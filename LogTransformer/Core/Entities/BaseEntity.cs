﻿using System;

namespace DevFreela.Core.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreatedAt = DateTime.Now;
        }

        public int Id { get; set; }
        public DateTime CreatedAt { get; private set; }
    }
}
