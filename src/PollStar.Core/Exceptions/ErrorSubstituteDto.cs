﻿namespace PollStar.Core.Exceptions;

public class ErrorSubstituteDto
{
    public string Field { get; }
    public string Value { get; }

    public ErrorSubstituteDto(string field, string value)
    {
        Field = field;
        Value = value;
    }
}