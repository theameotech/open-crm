﻿using OpenCRM.BizLogic.Helpers.Interfaces;
using OpenCRM.Common.DTO;
using OpenCRM.DB.DomainObjects;
using OpenCRM.DB.Repository;
using OpenCRM.DB.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.BizLogic.Helpers.Impl
{
    public class InboxHelper : IInboxHelper
    {

        private IInboxRepo _inboxRepo;
        private IUserRepo _userRepo;

        public InboxHelper(IInboxRepo inboxRepo, IUserRepo userRepo)
        {
            _inboxRepo = inboxRepo;
            _userRepo = userRepo;
        }
        public IList<Inbox> GetEmails(int userId, int companyId)
        {
            return _inboxRepo.FetchAll(x => x.UserID == userId && x.CompanyID == companyId);
        }

        public void SendEmail(Inbox inbox)
        {


            Inbox inb = new Inbox();
            inb.CompanyID = inbox.CompanyID;

            inb.EmailRecipient = inbox.EmailRecipient;
            inb.EmailSender = inbox.EmailSender;
            inb.EmailSubject = inbox.EmailSubject;
            inb.IsAttachment = inbox.IsAttachment;
            inb.IsDraft = inbox.IsDraft;
            inb.IsFlagged = inbox.IsFlagged;
            inb.IsForwarded = inbox.IsForwarded;
            inb.IsRead = inbox.IsRead;
            inb.IsTrash = inbox.IsTrash;
            inb.UserID = inbox.UserID;
            inb.IsReply = inbox.IsReply;
            inb.EmailContent = inbox.EmailContent;
            inb.AttachmentName = inbox.AttachmentName;
            inb.AttachmentType = inbox.AttachmentType;
            inb.SystemDate = inbox.SystemDate;
            var user = _userRepo.Get(x => x.UserEmail == inbox.EmailRecipient);
            inb.ToUserName = user.FirstName;
            inb.EmailID = user.Id;
            inb.FromUserName = inbox.FromUserName;
            inb.IPAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();
            inb.ServerDate = DateTime.Now;
            _inboxRepo.Add(inb);

        }


        public void FlagEmail(Inbox inbox)
        {
            var userEmails = _inboxRepo.Get(x => x.StatusID == inbox.StatusID);

            userEmails.IsFlagged = inbox.IsFlagged;
            _inboxRepo.Update(userEmails);
        }

        public void DeleteEmails(string[] inbox)
        {
            

            foreach (var c in inbox)
            {
                var userEmails = _inboxRepo.Get(x => x.StatusID == Convert.ToInt32(c));
                userEmails.IsTrash = true;
                _inboxRepo.Update(userEmails);
            }
            
        }


        public Inbox GetEmailBySenderId(int senderId)
        {
            return _inboxRepo.Get(x => x.EmailID == senderId);
        }


        public void ReadEmail(Inbox inbox)
        {

            var userEmails = _inboxRepo.Get(x => x.StatusID == inbox.StatusID);
            userEmails.IsFlagged = inbox.IsFlagged;
            _inboxRepo.Update(userEmails);

        }
    }
}
