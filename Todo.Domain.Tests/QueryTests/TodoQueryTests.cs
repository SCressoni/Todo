using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Todo.Domain.Entities;
using Todo.Domain.Queries;

namespace Todo.Domain.Tests.QueryTests;

[TestClass]
public class TodoQueryTests
{
    private List<TodoItem> _items;

    public TodoQueryTests()
    {
        _items = new List<TodoItem>();
        _items.Add(new TodoItem("Tarefa 1", "usuarioA", DateTime.Now));
        _items.Add(new TodoItem("Tarefa 2", "usuarioB", DateTime.Now));
        _items.Add(new TodoItem("Tarefa 3", "silviocressoni", DateTime.Now));
        _items.Add(new TodoItem("Tarefa 4", "usuarioD", DateTime.Now));
        _items.Add(new TodoItem("Tarefa 5", "silviocressoni", DateTime.Now));
    }
    
    [TestMethod]
    public void Dada_a_consulta_deve_retornar_tarefas_apenas_do_usuario_silviocressoni()
    {
        // Inclui o ASQUERYBLE() porque nao é possivel colocar uma expression dentro de um where.
        var result = _items.AsQueryable().Where(TodoQueries.GetAll("silviocressoni"));
        Assert.AreEqual(2, result.Count());
    }
}