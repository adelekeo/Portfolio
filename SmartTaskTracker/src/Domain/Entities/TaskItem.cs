using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class TaskItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public TaskStatus Status { get; set; } = TaskStatus.Todo;
    public DateTime? DueUtc { get; set; }
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    public Guid? AssigneeId { get; set; }
}
public enum TaskStatus { Todo, InProgress, Done, Blocked }
