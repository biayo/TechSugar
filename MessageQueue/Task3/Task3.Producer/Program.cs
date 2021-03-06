﻿using System;
using System.Messaging;

namespace Task3.Producer
{
    //
    //  References:
    //  http://stackoverflow.com/questions/10435706/msmq-cannot-receive-from-multicast-queues
    //  http://technet.microsoft.com/en-us/library/cc756156(WS.10).aspx
    //  http://flylib.com/books/en/2.855.1.71/1/
    //  http://www.jroller.com/mrettig/entry/msmq_3_0_and_multicasting
    //
    //  1. Multi cast support should be configured via Windows components.
    //  2. Should be manully created private queue 
    //     "I manually created a non-transactional private queue called MulticastTest and then set the Multicast address to 234.1.1.1:8001"
    //
    //  See short settings procedure in https://github.com/constructor-igor/TechSugar/wiki/MessageQueue or screens in subolder <solution>\Task3\Screens
    //
    class Program
    {
        const string QUEUE_PATH = @"formatname:MULTICAST=234.1.1.1:8001";
        static void Main()
        {
            using (MessageQueue mq = new MessageQueue(QUEUE_PATH))
            {
                Console.WriteLine("Format name: {0}", mq.FormatName); 
                bool exit;
                do
                {
                    Console.Write("enter command or 'exit' > ");
                    string command = Console.ReadLine() ?? "";
                    string[] commands = command.Split(' ');

                    try
                    {
                        switch (commands[0])
                        {
                            case "send":
                                var customerObject = new ProducerMessage(commands[1], Int32.Parse(commands[2]));
                                var message = new Message
                                {
                                    Priority = MessagePriority.Normal,
                                    Label = "",
                                    Body = customerObject
                                };
                                mq.Send(message);
                                Console.WriteLine("sent message '{0}, {1}'", customerObject.Text, customerObject.Duration);
                                break;
                        }
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine("failed with '{0}' message", e.Message);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("check command '{0}' arguments", commands[0]);
                    }
                    exit = String.IsNullOrWhiteSpace(command) || command.ToLower() == "exit";
                } while (!exit);
            }
        }
    }
}
