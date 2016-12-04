using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using ReactChat.Models;

namespace ReactChat.App_Start
{
    public class ChatManager
    {
        private ChatStore _chatStore;
        public ChatManager(ChatStore chatStore)
        {
            _chatStore = chatStore;
        }

        public void AddChat(ChatItem chatItem)
        {
            _chatStore.ChatList.Add(chatItem);
        }

        public void AddUser(String userName)
        {
            _chatStore.UserList.Add(userName);
        }

		public List<String> GetAllUsers()
		{
			return _chatStore.UserList;
		}

        public List<ChatItem> GetAllChat()
        {
            return _chatStore.ChatList;
        }
    }
}