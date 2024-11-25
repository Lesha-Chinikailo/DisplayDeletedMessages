using System;
using System.Collections.Generic;

namespace DisplayDeletedMessages.Models;

public partial class Deletedmessage
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime? DatetimeOfMessageSending { get; set; }

    public string TypeMessage { get; set; } = null!;

    public string MessageOrPath { get; set; } = null!;

    public string ReasonForDeleted { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
