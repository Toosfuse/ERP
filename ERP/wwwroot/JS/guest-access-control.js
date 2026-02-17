let pendingGuests = [];
let approvedGuests = [];

$(document).ready(function() {
    loadGuests();
    
    $('#searchPendingGuests').on('input', function() {
        const query = $(this).val().toLowerCase();
        filterGuests(query, 'pending');
    });
    
    $('#searchApprovedGuests').on('input', function() {
        const query = $(this).val().toLowerCase();
        filterGuests(query, 'approved');
    });
    
    $('#approveAllBtn').click(function() {
        approveAllGuests();
    });
    
    $('#rejectAllBtn').click(function() {
        rejectAllGuests();
    });
    
    $(document).on('click', '.approve-btn', function() {
        const guestId = $(this).data('guest-id');
        approveGuest(guestId);
    });
    
    $(document).on('click', '.reject-btn', function() {
        const guestId = $(this).data('guest-id');
        rejectGuest(guestId);
    });
});

function loadGuests() {
    $.get('/Chat/GetAllGuests', function(data) {
        pendingGuests = data.pending || [];
        approvedGuests = data.approved || [];
        renderGuests();
    }).fail(function() {
        alert('خطا در بارگذاری مهمانها');
    });
}

function renderGuests() {
    renderPendingGuests();
    renderApprovedGuests();
    updateCounts();
}

function renderPendingGuests() {
    let html = '';
    pendingGuests.forEach(function(guest) {
        html += `
            <div class="user-card available-card" data-guest-id="${guest.id}">
                <div class="user-info">
                    <img src="${guest.image}" alt="${guest.name}" class="user-avatar" />
                    <div>
                        <span class="user-name">${guest.name}</span>
                        <span class="user-phone">${guest.phoneNumber}</span>
                    </div>
                </div>
                <button class="approve-btn" data-guest-id="${guest.id}"><i class="fa fa-check"></i> تایید</button>
            </div>
        `;
    });
    $('#pendingGuestsList').html(html || '<p class="empty-message">مهمانی در انتظار نیست</p>');
}

function renderApprovedGuests() {
    let html = '';
    approvedGuests.forEach(function(guest) {
        html += `
            <div class="selected-card" data-guest-id="${guest.id}">
                <div class="user-info">
                    <img src="${guest.image}" alt="${guest.name}" class="user-avatar" />
                    <div>
                        <span class="user-name">${guest.name}</span>
                        <span class="user-phone">${guest.phoneNumber}</span>
                    </div>
                </div>
                <button class="reject-btn" data-guest-id="${guest.id}"><i class="fa fa-times"></i> رد</button>
            </div>
        `;
    });
    $('#approvedGuestsList').html(html || '<p class="empty-message">مهمانی تایید نشده است</p>');
}

function approveGuest(guestId) {
    $.post('/Chat/ApproveGuest', {
        guestId: guestId,
        __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
    }, function(result) {
        if (result.success) {
            loadGuests();
        } else {
            alert(result.error || 'خطا در تایید');
        }
    }).fail(function() {
        alert('خطا در تایید مهمان');
    });
}

function rejectGuest(guestId) {
    $.post('/Chat/RejectGuest', {
        guestId: guestId,
        __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
    }, function(result) {
        if (result.success) {
            loadGuests();
        } else {
            alert(result.error || 'خطا در رد');
        }
    }).fail(function() {
        alert('خطا در رد مهمان');
    });
}

function approveAllGuests() {
    if (pendingGuests.length === 0) return;
    
    if (confirm('آیا مطمئن هستید که میخواهید همه را تایید کنید؟')) {
        const guestIds = pendingGuests.map(g => g.id);
        
        $.post('/Chat/ApproveMultipleGuests', {
            guestIds: guestIds,
            __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
        }, function(result) {
            if (result.success) {
                loadGuests();
            } else {
                alert(result.error || 'خطا در تایید همه');
            }
        }).fail(function() {
            alert('خطا در تایید همه مهمانها');
        });
    }
}

function rejectAllGuests() {
    if (approvedGuests.length === 0) return;
    
    if (confirm('آیا مطمئن هستید که میخواهید همه را رد کنید؟')) {
        $.post('/Chat/RejectAllGuests', {
            __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
        }, function(result) {
            if (result.success) {
                loadGuests();
            } else {
                alert(result.error || 'خطا در رد همه');
            }
        }).fail(function() {
            alert('خطا در رد همه مهمانها');
        });
    }
}

function filterGuests(query, type) {
    if (query === '') {
        $(`.${type === 'pending' ? 'available-card' : 'selected-card'}`).show();
        return;
    }
    
    $(`.${type === 'pending' ? 'available-card' : 'selected-card'}`).each(function() {
        const name = $(this).find('.user-name').text().toLowerCase();
        const phone = $(this).find('.user-phone').text().toLowerCase();
        if (name.includes(query) || phone.includes(query)) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });
}

function updateCounts() {
    $('#pendingCount').text(pendingGuests.length);
    $('#approvedCount').text(approvedGuests.length);
}
