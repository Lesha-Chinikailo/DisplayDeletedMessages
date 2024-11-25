using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DisplayDeletedMessages.Models;

public partial class Deletedmessage
{
    public int Id { get; set; }
    
    [Browsable(false)]
    public int UserId { get; set; }

    public string Username
    {
        get
        {
            string name = BotAdministratorContext.getInstance().Users.Where(u => u.Id == UserId).Single().Name;
            if (name == null)
            {
                return BotAdministratorContext.getInstance().Users.Where(u => u.Id == UserId).Single().Username;
            }
            return name;
        }
    }

    public DateTime? DatetimeOfMessageSending { get; set; }

    public string TypeMessage { get; set; } = null!;

    public string MessageOrPath { get; set; } = null!;

    public string ReasonForDeleted { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
