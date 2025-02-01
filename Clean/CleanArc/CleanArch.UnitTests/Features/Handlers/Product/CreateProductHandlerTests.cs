using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.UnitTests.Features.Handlers.Product
{
    public class CreateProductHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IProductReadRepository> _productReadRepository;
        private readonly Mock<IProductWriteRepository> _productWriteRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly CreateProductHandler _handler;
        public CreateProductHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _mapper = new Mock<IMapper>();
            _productWriteRepository = new Mock<IProductWriteRepository>();
            _productReadRepository = new Mock<IProductReadRepository>();

            _handler = new CreateProductHandler(_unitOfWork.Object, _productWriteRepository.Object, _productReadRepository.Object, _mapper.Object);
        }
        [Fact]
        public async Task Handle_WhenValidatorIsFailed_ShouldThrowValidationException()
        {
            var request = CreateProductHandlerMockData.CreateProductCommandV1;

            Func<Task> func = async () => await _handler.Handle(request, It.IsAny<CancellationToken>());

            var exception = await Assert.ThrowsAsync<ValidationException>(func);
            Assert.Contains("Sekil duzgun daxil edilmelidir", exception.Errors);
        }
        [Fact]
        public async Task Handle_WhenProductAlreadyExist_SHouldThrowValidationException()
        {
            var request = CreateProductHandlerMockData.CreateProductCommandV2;

            _productReadRepository.Setup(x => x.GetByNameAsync(request.Name))
                .ReturnsAsync(new Product());

            Func<Task> func = async () => await _handler.Handle(request, It.IsAny<CancellationToken>());

            var exception = await Assert.ThrowsAsync<ValidationException>(func);
            Assert.Contains("Bu adda mehsul movcuddur", exception.Errors);
        }
        [Fact]
        public async Task Handle_WhenLowIsSucceeded_SchouldReturnResponseModel()
        {
            var request = CreateProductHandlerMockData.CreateProductCommandV2;
            _productReadRepository.Setup(x => x.GetByNameAsync(request.Name))
                .ReturnsAsync(value: null);

            _mapper.Setup(x => x.Map<Common.Entities.Product>(request))
                .Returns(new Product());

            var response = await _handler.Handle(request, It.IsAny<CancellationToken>());

            Assert.IsType<Response>(response);
            Assert.Equal("Mehsul ugurla elave olundu", response.Message);

        }
    }
}
