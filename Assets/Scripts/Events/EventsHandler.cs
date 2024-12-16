using System;

public static class EventsHandler
{
    public static event Action OnBeforeMove;

    public static void CallOnBeforeMoveEvent()
    {
        if(OnBeforeMove != null)
        {
            OnBeforeMove();
        }
    }


    public static event Action<bool> OnGroundStateChanged;

    //Buras? Publisherlar için olan k?s?m
    public static void CallOnGroundStateChangedEvent(bool onGround)
    {
        //bu evente subscribe olan birisi var m? diye kontrol ediyor
        if (OnGroundStateChanged != null)
        {
            OnGroundStateChanged(onGround);
        }
    }
}
