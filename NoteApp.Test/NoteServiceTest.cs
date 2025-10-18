using Moq;
using NoteApp.Logic.Domain.Entity.Note.Models;
using NoteApp.Logic.Domain.Entity.Note.Repository.Interface;
using NoteApp.Logic.Domain.Entity.Note.Service;

namespace NoteApp.Test;

[TestFixture]
public class NoteServiceTests
{
    private Mock<INoteEntityRepository> _repositoryMock = null!;
    private NoteService _service = null!;

    [SetUp]
    public void Setup()
    {
        _repositoryMock = new Mock<INoteEntityRepository>();
        _service = new NoteService(_repositoryMock.Object);
    }
    
    // Moq or (MOCK): uses the Arrange, Act, and Assert Mythology.

    [Test]
    public async Task GetAllAsync_ShouldReturnAlNotes()
    {
        // Arrange
        var fakeNotes = new List<NoteEntity>
        {
            new () {Id = 1, Title = "Test 1", Content = "Content 1"},
            new () {Id = 2, Title = "Test 2", Content = "Content 2"}
        };
        
        // Act
        _repositoryMock.Setup( r=> r.GetAllNotes(It.IsAny<CancellationToken>()))
            .ReturnsAsync(fakeNotes);
        
        var result = await _service.GetAllAsync(CancellationToken.None);
        
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result[0].Title, Is.EqualTo("Test 1"));
    }

    [Test]
    public async Task CreateNote_shouldReturnCreatedNote()
    {
        // Arrange (Mocking Data)
        var title = "Test 1";
        var content = "Content 1";
        var note = new NoteEntity{ Id = 1, Title = title, Content = content};
        
        _repositoryMock.Setup(r => r.AddAsync(It.IsAny<NoteEntity>(), It.IsAny<CancellationToken>()));
        
        // Act (Using the service to call actions)
        var result = _service.CreateNoteAsync(title, content);
        
        // Assert (veryfying things have worked)
        Assert.That(result.Result.Title, Is.Not.Null);
        Assert.That(result.Result.Content, Is.Not.Null);
        _repositoryMock.Verify(r => r.AddAsync(It.Is<NoteEntity>(n => n.Title == title), It.IsAny<CancellationToken>()), Times.Once());
    }

    [Test]
    public async Task GetByIdAsync_ShouldReturnANote_WhenNoteExist()
    {
        var note = new NoteEntity { Id = 5, Title = "Find Me", Content = "Content 1" };
        _repositoryMock.Setup(r => r.GetNoteById(5, It.IsAny<CancellationToken>()))
            .ReturnsAsync(note);
        
        var result = await _service.GetByIdAsync(5);
        
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(5));
        Assert.That(result.Title, Is.EqualTo("Find Me"));
        Assert.That(result.Content, Is.EqualTo("Content 1"));
    }

    [Test]
    public async Task GetByIdAsync_ShouldReturnNull_WhenNoteDoesNotExist()
    {
        _repositoryMock.Setup(r => r.GetNoteById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((NoteEntity?)null);
        
        
        var result = await _service.GetByIdAsync(555);
        
        Assert.That(result, Is.Null);
        
    }
}
