using AutoMapper;
using BookHaven.Application.Interface.Service;
using Microsoft.AspNetCore.Mvc;

namespace BookHaven.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
      
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderService orderService, IMapper mapper, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _mapper = mapper;
            _logger = logger;
        }
    }
    }

