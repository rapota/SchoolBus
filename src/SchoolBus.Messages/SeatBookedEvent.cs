namespace SchoolBus.Messages;

public sealed class SeatBookedEvent
{
    public int BookingId { get; }

    public SeatBookedEvent(int bookingId)
    {
        BookingId = bookingId;
    }
}