﻿namespace BH.Common.Dtos
{
    public class ErrorDto
    {
        public string Message { get; set; }

        public ErrorDto()
        {
        }

        public ErrorDto(string msg)
        {
            Message = msg;
        }
    }
}
