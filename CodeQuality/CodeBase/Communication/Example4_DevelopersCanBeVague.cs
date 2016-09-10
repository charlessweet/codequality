using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaughingMonkey.CodeQuality.Communication
{
    public class Example4_DevelopersCanBeVague
    {
        public static class EnvelopeRepository
        {
            public static Envelope GetEnvelope(int envelopeId)
            {
                return new Envelope()
                {
                    EnvelopeId = envelopeId,
                    Content = "This is the envelope from the local cache."
                };
            }

            public static Envelope RefreshEnvelope(int envelopeId)
            {
                return new Envelope()
                {
                    EnvelopeId = envelopeId,
                    Content = "This is the envelope from the database."
                };
            }
        }
        public class Envelope
        {
            public int EnvelopeId { get; set; }
            public string Content { get; set; }
            public override bool Equals(object obj)
            {
                if(obj is Envelope && obj != null)
                {
                    Envelope a = obj as Envelope;
                    return (a.EnvelopeId == this.EnvelopeId && a.Content == this.Content);
                }
                return base.Equals(obj);
            }
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }
        public bool Validate(Envelope envelope)
        {
            Envelope otherEnvelope = EnvelopeRepository.GetEnvelope(envelope.EnvelopeId);
            bool areEqual = envelope.Equals(otherEnvelope);
            Envelope thirdEnvelope = null;
            //twenty lines of business rules acting on these
            //for brevity - using Lorem Ipsum
            /*
                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed felis est, 
                posuere sit amet eros quis, tincidunt scelerisque arcu. Fusce sed arcu 
                euismod, rhoncus nunc at, molestie mi. Mauris ipsum augue, dictum ut 
                consequat at, tincidunt ac metus. Aliquam molestie diam risus, quis 
                molestie lectus venenatis in. Mauris tincidunt metus efficitur nulla 
                imperdiet semper. Nunc pharetra in ipsum eget egestas. Suspendisse 
                potenti. Quisque nec dapibus odio. Cum sociis natoque penatibus et 
                magnis dis parturient montes, nascetur ridiculus mus. Vestibulum eget 
                ligula ut urna finibus luctus. Curabitur ornare ipsum neque, et maximus 
                nibh hendrerit cursus. Maecenas erat libero, posuere a mattis in, egestas 
                vitae mi. Maecenas sollicitudin neque ac mi tempor posuere. Sed vitae turpis 
                quis ex tempor viverra.

                Done with Lorem Ipsum.  At this point you're probably bored and snoozing,
                so you may not be reading this. If you are, congratulations!  This comment
                block represents various operations which happen on the envelopes we've 
                declared above.  I picked 'Envelope' because we have such a concept at 
                DocuSign, though it's implemented much differently than the above would
                indicate.  Anything could have happened to these envelopes in the process -
                anything.  Imagine you haveto make a change in the following section.

                How many times would you have to scrollback up and then back down to 
                make sure you understood what each envelope is for?
            */
            if (!areEqual)
            {
                thirdEnvelope = EnvelopeRepository.RefreshEnvelope(envelope.EnvelopeId);
            }
            if (thirdEnvelope.Equals(envelope) || thirdEnvelope.Equals(otherEnvelope))
                return true;
            return false;
        }
    }
}
