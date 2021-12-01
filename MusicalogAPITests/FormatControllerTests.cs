using Moq;
using MusicalogAPI.Controllers.Musicalog;
using MusicalogAPI.Interfaces.Musicalog.Repositories;
using MusicalogAPI.Models.Musicalog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MusicalogAPITests;

public class FormatControllerTests
{
    private readonly Mock<IReadOnlyRepository<Format>> _mockFormatRepository;
    private readonly FormatController _formatController;
    private readonly List<Format> _testData;

    public FormatControllerTests()
    {
        _mockFormatRepository = new Mock<IReadOnlyRepository<Format>>();
        _formatController = new FormatController(_mockFormatRepository.Object);

        _testData = new List<Format>() {
            new Format() { Id = 1, Name = "CD" },
            new Format() { Id = 2, Name = "DVD" }
        };
    }

    [Fact]
    public void GetAsync_ReturnsIEnumerableTaskOfFormats()
    {
        _mockFormatRepository.Setup(r => r.GetAsync()).ReturnsAsync(_testData);

        var result = _formatController.GetAsync();

        Assert.IsType<Task<IEnumerable<Format>>>(result);
    }

    [Fact]
    public void GetAsync_ReturnsCorrectNumberOfFormats()
    {
        _mockFormatRepository.Setup(r => r.GetAsync()).ReturnsAsync(_testData);

        var result = _formatController.GetAsync();

        Assert.Equal(2, result.Result.Count());
    }
}