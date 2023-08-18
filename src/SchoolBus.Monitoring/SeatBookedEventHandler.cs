using Rebus.Handlers;
using SchoolBus.Messages;

namespace SchoolBus.Monitoring;

public class SeatBookedEventHandler : IHandleMessages<SeatBookedEvent>
{
    public async Task Handle(SeatBookedEvent message)
    {
    }
}