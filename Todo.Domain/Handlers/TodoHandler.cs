using Flunt.Notifications;
using Todo.Domain.Commands;
using Todo.Domain.Commands.Contracts;
using Todo.Domain.Entities;
using Todo.Domain.Handlers.Contracts;
using Todo.Domain.Repositories;

namespace Todo.Domain.Handlers;

public class TodoHandler : Notifiable, 
                            IHandler<CreateTodoCommand>, 
                            IHandler<UpdateTodoCommand>,
                            IHandler<MarkTodoAsDoneCommand>,
                            IHandler<MarkTodoAsUndoneCommand>
{
    private readonly ITodoRepository _respository;

    public TodoHandler(ITodoRepository repository)
    {
        _respository = repository;
    }
    
    public ICommandResult Handle(CreateTodoCommand command)
    {
        // fail fast validation
        command.Validate();
        if (command.Invalid)
            return new GenericCommandResult(false, "Ops, parece que sua tarefa esta errada !", command.Notifications);

        // Gera o TodoItem
        var todo = new TodoItem(command.Title, command.User, command.Date);
        
        // Salva no banco
        _respository.Create(todo);
        
        // retorna o resultado
        return new GenericCommandResult(true, "Tarefa salva", todo);
    }

    public ICommandResult Handle(UpdateTodoCommand command)
    {
        // fail fast validation
        command.Validate();
        if (command.Invalid)
            return new GenericCommandResult(false, "Ops, parece que sua tarefa esta errada !", command.Notifications);

        // Gera o TodoItem (Rehidratar)
        var todo = _respository.GetById(command.Id, command.User);
        
        // Altera o titulo
        todo.UpdateTitle(command.Title);

        // Salva no banco
        _respository.Update(todo);
        
        // retorna o resultado
        return new GenericCommandResult(true, "Tarefa salva", todo);
    }


    public ICommandResult Handle(MarkTodoAsDoneCommand command)
    {
        // fail fast validation
        command.Validate();
        if (command.Invalid)
            return new GenericCommandResult(false, "Ops, parece que sua tarefa esta errada !", command.Notifications);

        // recupera o TodoItem
        var todo = _respository.GetById(command.Id, command.User);
        
        // Altera o estado
        todo.MarkAsDone();

        // Salva no banco
        _respository.Update(todo);
        
        // retorna o resultado
        return new GenericCommandResult(true, "Tarefa salva", todo);
    }

    public ICommandResult Handle(MarkTodoAsUndoneCommand command)
    {
        // fail fast validation
        command.Validate();
        if (command.Invalid)
            return new GenericCommandResult(false, "Ops, parece que sua tarefa esta errada !", command.Notifications);

        // recupera o TodoItem
        var todo = _respository.GetById(command.Id, command.User);
        
        // Altera o estado
        todo.MarkAsUnDone();

        // Salva no banco
        _respository.Update(todo);
        
        // retorna o resultado
        return new GenericCommandResult(true, "Tarefa salva", todo);
    }
}