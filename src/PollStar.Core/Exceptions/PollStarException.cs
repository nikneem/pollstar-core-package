﻿using PollStar.Core.ErrorCodes;

namespace PollStar.Core.Exceptions;

public class PollStarException : Exception
{
    public PollStarErrorCode ErrorCode { get; }
    public List<ErrorSubstituteDto> Substitutes { get; }

    protected PollStarException(PollStarErrorCode errorCode, string message, Exception? ex) : base(message, ex)
    {
        ErrorCode = errorCode;
    }

}