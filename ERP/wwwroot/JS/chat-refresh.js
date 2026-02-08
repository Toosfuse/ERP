// اضافه کردن RefreshUsersList event handler
$(document).ready(function() {
    if (window.connection) {
        connection.on("RefreshUsersList", function () {
            $.get('/Chat/GetChatUsers', function(users) {
                let html = '';
                users.forEach(function(user) {
                    html += `
                        <div class="user-item" data-user-id="${user.id}" data-user-name="${user.name}">
                            <div class="user-avatar">
                                <img src="${user.image}" alt="${user.name}" />
                                <span class="online-status"></span>
                            </div>
                            <div class="user-info">
                                <div class="user-name">${user.name}</div>
                                <div class="last-message">${user.lastMessage || 'پیامی وجود ندارد'}</div>
                            </div>
                            ${user.unreadCount > 0 ? `<div class="unread-count">${user.unreadCount}</div>` : ''}
                        </div>
                    `;
                });
                $('#usersList').html(html);
            });
        });
    }
});
