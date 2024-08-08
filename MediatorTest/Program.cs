using System;
using Coding.Exercise;

var mediator = new Mediator();
var part1 = new Participant(mediator);
var part2 = new Participant(mediator);

part1.Say(3);
Console.WriteLine($"Participant 1: {part1.Value}, Participant 2: {part2.Value}");

part2.Say(2);
Console.WriteLine($"Participant 1: {part1.Value}, Participant 2: {part2.Value}");


namespace Coding.Exercise
{
    public class Participant
    {
        private Mediator _mediator;

        public int Value { get; set; } = 0;

        public Participant(Mediator mediator)
        {
            _mediator = mediator;
            _mediator.AddParticipant(this);
        }

        public void Say(int n)
        {
            _mediator.SetValue(n, this);
        }
    }

    public class Mediator
    {
        private List<Participant> _participants = new List<Participant>();

        public void AddParticipant(Participant participant)
        {
            _participants.Add(participant);
        }
        
        public void SetValue(int n, Participant speaker)
        {
            foreach (var participant in _participants)
            {
                if (participant != speaker)
                {
                    participant.Value = n;
                }
            }
        }
    }
}