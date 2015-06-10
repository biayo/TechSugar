using System;

namespace Demo
{
    public class Worker
    {
        private readonly MailService m_mailService;
        private readonly Repository m_repository;
        private int m_count;
        static readonly object LockedObject = new object();

        public Worker()
        {
            m_mailService = new MailService();
            m_repository = new Repository();
        }

        public string Run(string request)
        {
            lock (LockedObject)
            {
                string message = String.Format("{0}: {1}", m_count, request);
                m_count = m_count + 1;
                m_repository.Save(message);
                m_mailService.Send(message);
                return message;
            }
        }
    }
}