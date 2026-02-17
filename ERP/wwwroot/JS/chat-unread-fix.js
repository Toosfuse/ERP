// اضافه کردن به chat.js - بخش connection.on

connection.on("UpdateUnreadCount", function (count) {
    // آپدیت تعداد نخوانده در لیست کاربران
    if (count > 0) {
        if ($('.unread-badge').length === 0) {
            $('body').append(`<div class="unread-badge" style="position:fixed;top:10px;right:10px;background:#f44336;color:white;padding:8px 12px;border-radius:20px;font-weight:bold;z-index:9999;">${count}</div>`);
        } else {
            $('.unread-badge').text(count);
        }
    }
});

// بروزرسانی تعداد نخوانده برای هر کاربر
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

// بروزرسانی تعداد نخوانده برای گروه
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
