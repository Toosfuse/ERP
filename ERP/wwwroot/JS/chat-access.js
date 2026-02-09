let allAvailableUsers = [];
let selectedUserId = null;
let allowedUsers = [];

$(document).ready(function() {
    const preSelectedUserId = $('#preSelectedUserId').val();
    if (preSelectedUserId) {
        selectedUserId = preSelectedUserId;
        loadUserData(preSelectedUserId);
    }
    
    $('#searchAvailableUsers').on('input', function() {
        const query = $(this).val().toLowerCase();
        filterAvailableUsers(query);
    });
    
    $('#searchAllowedUsers').on('input', function() {
        const query = $(this).val().toLowerCase();
        filterAllowedUsers(query);
    });
    
    $('#selectAllBtn').click(function() {
        selectAllUsers();
    });
    
    $('#removeAllBtn').click(function() {
        removeAllUsers();
    });
    
    $(document).on('click', '.add-btn', function() {
        const userId = $(this).data('user-id');
        addUserToAllowed(userId);
    });
    
    $(document).on('click', '.remove-btn', function() {
        const userId = $(this).data('user-id');
        removeUserFromAllowed(userId);
    });
});

function loadUserData(userId) {
    $.get('/Chat/GetUserInfo', { userId: userId }, function(user) {
        $('#currentUserName').text(user.name);
    });
    
    loadAllowedUsers(userId);
}

function loadAllowedUsers(userId) {
    $.get('/Chat/GetUserAllowedUsers', { userId: userId }, function(data) {
        allowedUsers = data.allowed;
        allAvailableUsers = data.available;
        renderAllowedUsers();
        renderAvailableUsers();
    }).fail(function() {
        alert('خطا در بارگذاری اعضا');
    });
}

function renderAvailableUsers() {
    let html = '';
    allAvailableUsers.forEach(function(user) {
        html += `
            <div class="user-card available-card" data-user-id="${user.id}">
                <div class="user-info">
                    <img src="${user.image}" alt="${user.name}" class="user-avatar" />
                    <span class="user-name">${user.name}</span>
                </div>
                <button class="add-btn" data-user-id="${user.id}"><i class="fa fa-plus"></i> افزودن</button>
            </div>
        `;
    });
    $('#availableUsersList').html(html || '<p class="empty-message">همه کاربران اضافه شده‌اند</p>');
}

function renderAllowedUsers() {
    let html = '';
    allowedUsers.forEach(function(user) {
        html += `
            <div class="selected-card" data-user-id="${user.id}">
                <div class="user-info">
                    <img src="${user.image}" alt="${user.name}" class="user-avatar" />
                    <span class="user-name">${user.name}</span>
                </div>
                <button class="remove-btn" data-user-id="${user.id}"><i class="fa fa-times"></i> حذف</button>
            </div>
        `;
    });
    $('#allowedUsersList').html(html || '<p class="empty-message">عضوی اضافه نشده است</p>');
}

function addUserToAllowed(userId) {
    if (!selectedUserId) return;
    
    $.post('/Chat/AddChatAccessForUser', {
        userId: selectedUserId,
        allowedUserId: userId,
        __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
    }, function(result) {
        if (result.success) {
            loadAllowedUsers(selectedUserId);
        }
    }).fail(function() {
        alert('خطا در افزودن');
    });
}

function removeUserFromAllowed(userId) {
    if (!selectedUserId) return;
    
    $.post('/Chat/RemoveChatAccessForUser', {
        userId: selectedUserId,
        allowedUserId: userId,
        __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
    }, function(result) {
        if (result.success) {
            loadAllowedUsers(selectedUserId);
        }
    }).fail(function() {
        alert('خطا در حذف');
    });
}

function selectAllUsers() {
    if (!selectedUserId || allAvailableUsers.length === 0) return;
    
    const userIds = allAvailableUsers.map(u => u.id);
    
    $.post('/Chat/AddMultipleChatAccess', {
        userId: selectedUserId,
        allowedUserIds: userIds,
        __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
    }, function(result) {
        if (result.success) {
            loadAllowedUsers(selectedUserId);
        }
    }).fail(function() {
        alert('خطا در افزودن همه');
    });
}

function removeAllUsers() {
    if (!selectedUserId || allowedUsers.length === 0) return;
    
    if (confirm('آیا مطمئن هستید که میخواهید همه را حذف کنید؟')) {
        $.post('/Chat/RemoveAllChatAccess', {
            userId: selectedUserId,
            __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
        }, function(result) {
            if (result.success) {
                loadAllowedUsers(selectedUserId);
            }
        }).fail(function() {
            alert('خطا در حذف همه');
        });
    }
}

function filterAvailableUsers(query) {
    if (query === '') {
        $('.available-card').show();
        return;
    }
    
    $('.available-card').each(function() {
        const userName = $(this).find('.user-name').text().toLowerCase();
        if (userName.includes(query)) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });
}

function filterAllowedUsers(query) {
    if (query === '') {
        $('.selected-card').show();
        return;
    }
    
    $('.selected-card').each(function() {
        const userName = $(this).find('.user-name').text().toLowerCase();
        if (userName.includes(query)) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });
}
