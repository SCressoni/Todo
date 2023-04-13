using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Todo.Domain.Commands;

namespace Todo.Domain.Tests;

[TestClass]
public class CreateTodoCommandTestes
{
    private readonly CreateTodoCommand _invalidCommand = new CreateTodoCommand("", "", DateTime.Now);
    private readonly CreateTodoCommand _validCommand = new CreateTodoCommand("Titula da Tarefa", "Silvio Cressoni", DateTime.Now);
    
    [TestMethod]
    public void Dado_um_comando_invalido()
    {
        _invalidCommand.Validate();
        Assert.AreEqual(_invalidCommand.Valid, false);
    }
    
    [TestMethod]
    public void Dado_um_comando_valido()
    {
        _validCommand.Validate();
        Assert.AreEqual(_validCommand.Valid, true);
    }
}