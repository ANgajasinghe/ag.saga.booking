using ag.saga.common.Events;
using ag.saga.common.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace ag.saga.booking.api.Controllers;

[ApiController]
[Route("[controller]")]
public class BookingController : ControllerBase
{
    private readonly ILogger<BookingController> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public BookingController(ILogger<BookingController> logger, IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }
    
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateBooking createBooking)
    {
        _logger.LogInformation("Booking is initiated {Booking}", createBooking);
        await _publishEndpoint.Publish<BookingInitializationEvent>(new
        {
            createBooking.BookingId,
            createBooking.Price
        });

        return Ok("Success");
    }
    
    
}