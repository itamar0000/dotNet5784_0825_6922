﻿namespace BO;
internal class MilestoneInList
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string Alias { get; set; }
    public Status? Status { get; set; }
    public double? CompletionPercentage { get; set; }
}
