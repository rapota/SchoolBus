using Rebus.Bus;
using Rebus.Handlers;
using SchoolBus.Messages;

namespace SchoolBus.Handlers;

public class BookSeatRequestHandler : IHandleMessages<BookSeatRequest>
{
    private readonly IBus _bus;

    public BookSeatRequestHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(BookSeatRequest message)
    {
        BookSeatResponse bookSeatResponse = new()
        {
            BookingId = 123
        };

        await _bus.Reply(bookSeatResponse);
    }
}