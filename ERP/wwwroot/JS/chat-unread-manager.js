// مدیریت تعداد پیامهای نخوانده

$(document).ready(function() {
    // بروزرسانی تعداد نخوانده برای هر کاربر وقتی پیام جدید میاد
    connection.on("ReceiveMessage", function (msg) {
        if (msg.receiverId === myUserId && currentUserId !== msg.senderId) {
            updateUserUnreadCount(msg.senderId, 1);
        }
    });

    // بروزرسانی تعداد نخوانده برای گروه
    connection.on("ReceiveGroupMessage", function (msg) {
        if (window.currentGroupId && msg.groupId === window.currentGroupId) {
            // اگر پیام در گروه فعلی باشد، نخوانده نیست
            return;
        }
        if (msg.senderId !== myUserId) {
            updateGroupUnreadCount(msg.groupId, 1);
        }
    });

    // حذف تعداد نخوانده وقتی کاربر پیام رو میخونه
    $(document).on('click', '.user-item:not(.group-item)', function() {
        const userId = $(this).data('user-id');
        $(this).find('.unread-count').remove();
    });

    // حذف تعداد نخوانده برای گروه
    $(document).on('click', '.group-item', function() {
        const groupId = $(this).data('group-id');
        $(this).find('.unread-count').remove();
    });
});

function updateUserUnreadCount(userId, increment = 1) {
    const userItem = $(`.user-item[data-user-id="${userId}"]`);
    const unreadEl = userItem.find('.unread-count');
    
    if (unreadEl.length) {
        const current = parseInt(unreadEl.text());
        unreadEl.text(current + increment);
    } else {
        userItem.append(`<div class="unread-count">${increment}</div>`);
    }
}

function updateGroupUnreadCount(groupId, increment = 1) {
    const groupItem = $(`.group-item[data-group-id="${groupId}"]`);
    const unreadEl = groupItem.find('.unread-count');
    
    if (unreadEl.length) {
        const current = parseInt(unreadEl.text());
        unreadEl.text(current + increment);
    } else {
        groupItem.append(`<div class="unread-count" style="background:#f44336;color:white;padding:2px 6px;border-radius:10px;font-size:11px;font-weight:bold;">${increment}</div>`);
    }
}
