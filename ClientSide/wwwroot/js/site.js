 // Toggle mobile menu
        document.getElementById('mobile-menu-button').addEventListener('click', function() {
            const menu = document.getElementById('mobile-menu');
            menu.classList.toggle('hidden');
        });
        
        // Simulate authentication status (in a real app, this would come from your backend)
        const isAuthenticated = false; // Change to true to see authenticated state
        const displayName = "علی محمدی";
        
        // Render auth buttons based on authentication status
        function renderAuthButtons() {
            const authButtonsContainer = document.getElementById('auth-buttons');
            const mobileAuthButtonsContainer = document.getElementById('mobile-auth-buttons');
            
            if (isAuthenticated) {
                // Authenticated state
                authButtonsContainer.innerHTML = `
                    <li class="signin-btn">
                        <a href="#" class="flex items-center text-blue-500 font-medium">
                            <i class="fas fa-user-circle ml-2"></i>
                            <span>${displayName}</span>
                        </a>
                    </li>
                `;
                
                mobileAuthButtonsContainer.innerHTML = `
                    <li class="signin-btn">
                        <a href="#" class="flex items-center text-blue-500 font-medium py-2">
                            <i class="fas fa-user-circle ml-2"></i>
                            <span>${displayName}</span>
                        </a>
                    </li>
                `;
            } else {
                // Not authenticated
                authButtonsContainer.innerHTML = `
                    <li class="nav-item">
                        <a href="#" class="text-gray-700 hover:text-blue-500">ثبت نام</a>
                    </li>
                    <li class="nav-item">
                        <a href="#" class="text-gray-700 hover:text-blue-500">ورود</a>
                    </li>
                `;
                
                mobileAuthButtonsContainer.innerHTML = `
                    <li>
                        <a href="#" class="block text-gray-700 hover:text-blue-500 py-2">ثبت نام</a>
                    </li>
                    <li>
                        <a href="#" class="block text-gray-700 hover:text-blue-500 py-2">ورود</a>
                    </li>
                `;
            }
        }
        
        // Initialize the page
        document.addEventListener('DOMContentLoaded', function() {
            renderAuthButtons();
            
            // Check for messages to show alerts
            const messages = {
                success: document.getElementById('message1').value,
                error: document.getElementById('message2').value,
                info: document.getElementById('message3').value,
                warning: document.getElementById('message4').value
            };
            
            for (const [type, message] of Object.entries(messages)) {
                if (message) {
                    Swal.fire({
                        icon: type,
                        title: message,
                        toast: true,
                        position: 'top-end',
                        showConfirmButton: false,
                        timer: 3000,
                        timerProgressBar: true
                    });
                }
            }
        });

// Add active class to clicked sidebar link
document.querySelectorAll('.sidebar-link').forEach(link => {
    link.addEventListener('click', function () {
        document.querySelectorAll('.sidebar-link').forEach(el => el.classList.remove('active'));
        this.classList.add('active');
        // حذف e.preventDefault() اجازه می‌دهد لینک‌ها به درستی کار کنند
    });
    console.log("52");
});

// Responsive menu toggle (for mobile)
function toggleMenu() {
    const sidebar = document.querySelector('.w-1/4');
    sidebar.classList.toggle('hidden');
    sidebar.classList.toggle('block');
}


// Toggle password visibility
function togglePasswordVisibility(inputId) {
    const input = document.getElementById(inputId);
    const icon = document.querySelector(`[onclick="togglePasswordVisibility('${inputId}')"] i`);

    if (input.type === 'password') {
        input.type = 'text';
        icon.classList.remove('fa-eye');
        icon.classList.add('fa-eye-slash');
    } else {
        input.type = 'password';
        icon.classList.remove('fa-eye-slash');
        icon.classList.add('fa-eye');
    }
}

// Check password strength
function checkPasswordStrength(password) {
    const meter = document.getElementById('passwordStrengthMeter');
    const text = document.getElementById('passwordStrengthText');
    const requirements = {
        length: password.length >= 8,
        uppercase: /[A-Z]/.test(password),
        lowercase: /[a-z]/.test(password),
        number: /[0-9]/.test(password)
    };

    // Update requirement icons
    updateRequirementIcon('lengthRequirement', requirements.length);
    updateRequirementIcon('uppercaseRequirement', requirements.uppercase);
    updateRequirementIcon('lowercaseRequirement', requirements.lowercase);
    updateRequirementIcon('numberRequirement', requirements.number);

    // Calculate strength score (0-4)
    const strengthScore = Object.values(requirements).filter(Boolean).length;

    // Update meter and text
    let strengthColor = '';
    let strengthText = '';

    switch (strengthScore) {
        case 0:
            meter.style.width = '0%';
            meter.style.backgroundColor = '#ef4444';
            strengthText = 'خیلی ضعیف';
            break;
        case 1:
            meter.style.width = '25%';
            meter.style.backgroundColor = '#ef4444';
            strengthText = 'ضعیف';
            break;
        case 2:
            meter.style.width = '50%';
            meter.style.backgroundColor = '#f59e0b';
            strengthText = 'متوسط';
            break;
        case 3:
            meter.style.width = '75%';
            meter.style.backgroundColor = '#3b82f6';
            strengthText = 'قوی';
            break;
        case 4:
            meter.style.width = '100%';
            meter.style.backgroundColor = '#10b981';
            strengthText = 'خیلی قوی';
            break;
    }

    text.textContent = `سطح امنیت: ${strengthText}`;
    text.className = `text-xs mt-1 ${strengthScore >= 3 ? 'text-green-500' : strengthScore >= 2 ? 'text-blue-500' : 'text-red-500'}`;
}

// Update requirement icon
function updateRequirementIcon(elementId, isMet) {
    const element = document.getElementById(elementId);
    const icon = element.querySelector('i');

    if (isMet) {
        icon.classList.remove('fa-circle', 'text-gray-400');
        icon.classList.add('fa-check-circle', 'text-green-500');
    } else {
        icon.classList.remove('fa-check-circle', 'text-green-500');
        icon.classList.add('fa-circle', 'text-gray-400');
    }
}

// Form validation
document.getElementById('passwordChangeForm').addEventListener('submit', function (e) {
    /*e.preventDefault();*/

    /*const currentPassword = document.getElementById('currentPassword');*/
    const newPassword = document.getElementById('newPassword');
    const confirmNewPassword = document.getElementById('confirmNewPassword');

    let isValid = true;

    // Reset errors
    document.querySelectorAll('.error-message').forEach(el => el.style.display = 'none');
    document.querySelectorAll('.input-error').forEach(el => el.classList.remove('input-error'));

   

    // Validate new password
    const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$/;
    if (!passwordRegex.test(newPassword.value)) {
        document.getElementById('newPasswordError').style.display = 'block';
        newPassword.classList.add('input-error');
        isValid = false;
    }

    // Validate password match
    if (newPassword.value !== confirmNewPassword.value) {
        document.getElementById('confirmPasswordError').style.display = 'block';
        confirmNewPassword.classList.add('input-error');
        isValid = false;
        e.preventDefault();
    }
    return true;

    // If valid, show success modal
    if (isValid) {
        document.getElementById('successModal').classList.remove('hidden');
        this.reset();
        document.getElementById('passwordStrengthMeter').style.width = '0%';
        document.getElementById('passwordStrengthText').textContent = '';

        // Reset requirement icons
        ['lengthRequirement', 'uppercaseRequirement', 'lowercaseRequirement', 'numberRequirement'].forEach(id => {
            const element = document.getElementById(id);
            const icon = element.querySelector('i');
            icon.classList.remove('fa-check-circle', 'text-green-500');
            icon.classList.add('fa-circle', 'text-gray-400');
        });
    }
});

// Close success modal
function closeSuccessModal() {
    document.getElementById('successModal').classList.add('hidden');
}













        // Sample data
    let users = [
    {
        id: 1,
    firstName: "محمد",
    lastName: "رضایی",
    phone: "09123456789",
    nationalId: "1234567890",
    studentId: "9823456",
    joinDate: "1402/05/15",
    status: "active"
            },
    {
        id: 2,
    firstName: "فاطمه",
    lastName: "محمدی",
    phone: "09129876543",
    nationalId: "0987654321",
    studentId: "9823457",
    joinDate: "1402/04/22",
    status: "active"
            },
    {
        id: 3,
    firstName: "علی",
    lastName: "کریمی",
    phone: "09351234567",
    nationalId: "1122334455",
    studentId: "9823458",
    joinDate: "1402/03/10",
    status: "inactive"
            },
    {
        id: 4,
    firstName: "زهرا",
    lastName: "احمدی",
    phone: "09123459876",
    nationalId: "5566778899",
    studentId: "9823459",
    joinDate: "1402/02/05",
    status: "pending"
            }
    ];

    let news = [
    {
        id: 1,
    title: "برگزاری همایش سالانه دانشگاه",
    description: "همایش سالانه دانشگاه در تاریخ 15 مرداد ماه برگزار خواهد شد. از کلیه دانشجویان و اساتید دعوت به عمل می‌آید.",
    image: "https://images.unsplash.com/photo-1523050854058-8df90110c9f1?ixlib=rb-1.2.1&auto=format&fit=crop&w=500&q=60"
            },
    {
        id: 2,
    title: "افتتاح آزمایشگاه جدید",
    description: "آزمایشگاه جدید فناوری اطلاعات با حضور ریاست دانشگاه افتتاح شد. این آزمایشگاه مجهز به جدیدترین تجهیزات است.",
    image: "https://images.unsplash.com/photo-1522202176988-66273c2fd55f?ixlib=rb-1.2.1&auto=format&fit=crop&w=500&q=60"
            },
    {
        id: 3,
    title: "برنامه زمانبندی امتحانات",
    description: "برنامه زمانبندی امتحانات پایان ترم نیمسال دوم سال تحصیلی 1401-1402 منتشر شد.",
    image: "https://images.unsplash.com/photo-1434030216411-0b793f4b4173?ixlib=rb-1.2.1&auto=format&fit=crop&w=500&q=60"
            }
    ];

    // DOM elements
    const usersTab = document.getElementById('users-tab');
    const newsTab = document.getElementById('news-tab');
    const usersContent = document.getElementById('users-content');
    const newsContent = document.getElementById('news-content');
    const mobileUsersTab = document.getElementById('mobile-users-tab');
    const mobileNewsTab = document.getElementById('mobile-news-tab');
    const openSidebar = document.getElementById('open-sidebar');
    const closeSidebar = document.getElementById('close-sidebar');
    const mobileSidebar = document.getElementById('mobile-sidebar');
    const sidebarBackdrop = document.getElementById('sidebar-backdrop');
    const usersTableBody = document.getElementById('users-table-body');
    const newsContainer = document.getElementById('news-container');
    const newUserBtn = document.getElementById('new-user-btn');
    const newNewsBtn = document.getElementById('new-news-btn');
    const userModal = document.getElementById('user-modal');
    const userModalOverlay = document.getElementById('user-modal-overlay');
    const userModalTitle = document.getElementById('user-modal-title');
    const userForm = document.getElementById('user-form');
    const userIdInput = document.getElementById('user-id');
    const firstNameInput = document.getElementById('first-name');
    const lastNameInput = document.getElementById('last-name');
    const phoneInput = document.getElementById('phone');
    const nationalIdInput = document.getElementById('national-id');
    const studentIdInput = document.getElementById('student-id');
    const joinDateInput = document.getElementById('join-date');
    const statusInput = document.getElementById('status');
    const newsModal = document.getElementById('news-modal');
    const newsModalOverlay = document.getElementById('news-modal-overlay');
    const newsModalTitle = document.getElementById('news-modal-title');
    const newsForm = document.getElementById('news-form');
    const newsIdInput = document.getElementById('news-id');
    const newsTitleInput = document.getElementById('news-title');
    const newsDescriptionInput = document.getElementById('news-description');
    const newsImageInput = document.getElementById('news-image');
    const newsImagePreview = document.getElementById('news-image-preview');
    const newsImagePreviewContainer = document.getElementById('news-image-preview-container');
    const deleteModal = document.getElementById('delete-modal');
    const deleteModalOverlay = document.getElementById('delete-modal-overlay');
    const deleteMessage = document.getElementById('delete-message');
    const confirmDeleteBtn = document.getElementById('confirm-delete');

    // Variables for tracking what to delete
    let currentDeleteId = null;
    let currentDeleteType = null; // 'user' or 'news'

    // Initialize the app
    document.addEventListener('DOMContentLoaded', function () {
        renderUsersTable();
    renderNewsCards();
    setupEventListeners();
        });

    // Render users table
    function renderUsersTable() {
        usersTableBody.innerHTML = '';

            users.forEach((user, index) => {
                const statusText = getStatusText(user.status);
    const statusClass = getStatusClass(user.status);

    const row = document.createElement('tr');
    row.innerHTML = `
    <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">${index + 1}</td>
    <td class="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">${user.firstName}</td>
    <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">${user.lastName}</td>
    <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">${user.phone}</td>
    <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">${user.nationalId}</td>
    <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">${user.studentId}</td>
    <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">${user.joinDate}</td>
    <td class="px-6 py-4 whitespace-nowrap">
        <span class="px-2 inline-flex text-xs leading-5 font-semibold rounded-full ${statusClass}">${statusText}</span>
    </td>
    <td class="px-6 py-4 whitespace-nowrap text-sm font-medium">
        <button class="text-yellow-600 hover:text-yellow-900 ml-2 edit-user-btn" data-user-id="${user.id}">
            <i class="fas fa-edit"></i>
        </button>
        <button class="text-red-600 hover:text-red-900 ml-2 delete-btn" data-id="${user.id}" data-type="user">
            <i class="fas fa-trash"></i>
        </button>
    </td>
    `;
    usersTableBody.appendChild(row);
            });
        }

    // Render news cards
    function renderNewsCards() {
        newsContainer.innerHTML = '';

            news.forEach(item => {
                const card = document.createElement('div');
    card.className = 'news-card bg-white rounded-lg shadow overflow-hidden';
    card.innerHTML = `
    <img src="${item.image}" alt="${item.title}" class="w-full news-card-image">
        <div class="p-4">
            <h3 class="font-bold text-lg mb-2">${item.title}</h3>
            <p class="text-gray-600 text-sm mb-4">${item.description}</p>
            <div class="flex justify-end space-x-2">
                <button class="text-yellow-600 hover:text-yellow-800 edit-news-btn" data-news-id="${item.id}">
                    <i class="fas fa-edit"></i>
                </button>
                <button class="text-red-600 hover:text-red-800 delete-btn" data-id="${item.id}" data-type="news">
                    <i class="fas fa-trash"></i>
                </button>
            </div>
        </div>
        `;
        newsContainer.appendChild(card);
            });
        }

        // Get status text
        function getStatusText(status) {
            switch (status) {
                case 'active': return 'فعال';
        case 'inactive': return 'غیرفعال';
        case 'pending': return 'در حال بررسی';
        default: return status;
            }
        }

        // Get status class
        function getStatusClass(status) {
            switch (status) {
                case 'active': return 'bg-green-100 text-green-800';
        case 'inactive': return 'bg-red-100 text-red-800';
        case 'pending': return 'bg-yellow-100 text-yellow-800';
        default: return 'bg-gray-100 text-gray-800';
            }
        }

        // Setup event listeners
        function setupEventListeners() {
            // Tab switching
            usersTab.addEventListener('click', (e) => {
                e.preventDefault();
                switchTab(usersTab, newsTab, usersContent, newsContent);
            });

            newsTab.addEventListener('click', (e) => {
            e.preventDefault();
        switchTab(newsTab, usersTab, newsContent, usersContent);
            });

            mobileUsersTab.addEventListener('click', (e) => {
            e.preventDefault();
        switchTab(mobileUsersTab, mobileNewsTab, usersContent, newsContent);
        mobileSidebar.style.display = 'none';
            });

            mobileNewsTab.addEventListener('click', (e) => {
            e.preventDefault();
        switchTab(mobileNewsTab, mobileUsersTab, newsContent, usersContent);
        mobileSidebar.style.display = 'none';
            });

            // Sidebar toggle
            openSidebar.addEventListener('click', () => {
            mobileSidebar.style.display = 'block';
            });

            closeSidebar.addEventListener('click', () => {
            mobileSidebar.style.display = 'none';
            });

            sidebarBackdrop.addEventListener('click', () => {
            mobileSidebar.style.display = 'none';
            });

            // User modal
            newUserBtn.addEventListener('click', () => {
            openUserModal();
            });

            // News modal
            newNewsBtn.addEventListener('click', () => {
            openNewsModal();
            });

            // User form submission
            userForm.addEventListener('submit', (e) => {
            e.preventDefault();
        saveUser();
            });

            // News form submission
            newsForm.addEventListener('submit', (e) => {
            e.preventDefault();
        saveNews();
            });

            // Image preview for news
            newsImageInput.addEventListener('change', (e) => {
                const file = e.target.files[0];
        if (file) {
                    const reader = new FileReader();
                    reader.onload = (event) => {
            newsImagePreview.src = event.target.result;
        newsImagePreviewContainer.style.display = 'block';
                    };
        reader.readAsDataURL(file);
                }
            });

            // Delete buttons (delegated)
            document.addEventListener('click', (e) => {
                // Edit user buttons
                if (e.target.closest('.edit-user-btn')) {
                    const userId = e.target.closest('.edit-user-btn').getAttribute('data-user-id');
        editUser(userId);
                }

        // Edit news buttons
        if (e.target.closest('.edit-news-btn')) {
                    const newsId = e.target.closest('.edit-news-btn').getAttribute('data-news-id');
        editNews(newsId);
                }

        // Delete buttons
        if (e.target.closest('.delete-btn')) {
                    const btn = e.target.closest('.delete-btn');
        const id = btn.getAttribute('data-id');
        const type = btn.getAttribute('data-type');
        confirmDelete(id, type);
                }
            });

            // Close modals
            document.querySelectorAll('.close-user-modal').forEach(btn => {
            btn.addEventListener('click', () => {
                userModal.classList.remove('active');
                userModalOverlay.classList.remove('active');
            });
            });

            document.querySelectorAll('.close-news-modal').forEach(btn => {
            btn.addEventListener('click', () => {
                newsModal.classList.remove('active');
                newsModalOverlay.classList.remove('active');
            });
            });

            document.querySelectorAll('.close-delete-modal').forEach(btn => {
            btn.addEventListener('click', () => {
                deleteModal.classList.remove('active');
                deleteModalOverlay.classList.remove('active');
            });
            });

            // Confirm delete
            confirmDeleteBtn.addEventListener('click', () => {
                if (currentDeleteType === 'user') {
            deleteUser(currentDeleteId);
                } else if (currentDeleteType === 'news') {
            deleteNews(currentDeleteId);
                }
        deleteModal.classList.remove('active');
        deleteModalOverlay.classList.remove('active');
            });
        }

        // Switch tabs
        function switchTab(selectedTab, otherTab, selectedContent, otherContent) {
            selectedTab.classList.add('active');
        otherTab.classList.remove('active');
        selectedContent.classList.remove('hidden');
        otherContent.classList.add('hidden');
        }

        // Open user modal for adding new user
        function openUserModal() {
            userIdInput.value = '';
        firstNameInput.value = '';
        lastNameInput.value = '';
        phoneInput.value = '';
        nationalIdInput.value = '';
        studentIdInput.value = '';
        joinDateInput.value = '';
        statusInput.value = 'active';
        userModalTitle.textContent = 'افزودن کاربر جدید';

        userModalOverlay.classList.add('active');
        userModal.classList.add('active');
        }

        // Edit user
        function editUser(userId) {
            const user = users.find(u => u.id == userId);
        if (user) {
            userIdInput.value = user.id;
        firstNameInput.value = user.firstName;
        lastNameInput.value = user.lastName;
        phoneInput.value = user.phone;
        nationalIdInput.value = user.nationalId;
        studentIdInput.value = user.studentId;
        joinDateInput.value = user.joinDate;
        statusInput.value = user.status;
        userModalTitle.textContent = 'ویرایش کاربر';

        userModalOverlay.classList.add('active');
        userModal.classList.add('active');
            }
        }

        // Save user (add or update)
        function saveUser() {
            const userData = {
            firstName: firstNameInput.value,
        lastName: lastNameInput.value,
        phone: phoneInput.value,
        nationalId: nationalIdInput.value,
        studentId: studentIdInput.value,
        joinDate: joinDateInput.value,
        status: statusInput.value
            };

        if (userIdInput.value) {
                // Update existing user
                const userId = parseInt(userIdInput.value);
                const userIndex = users.findIndex(u => u.id === userId);
        if (userIndex !== -1) {
            users[userIndex] = { ...users[userIndex], ...userData };
                }
            } else {
                // Add new user
                const newId = users.length > 0 ? Math.max(...users.map(u => u.id)) + 1 : 1;
        users.push({id: newId, ...userData });
            }

        renderUsersTable();
        userModal.classList.remove('active');
        userModalOverlay.classList.remove('active');
        }

        // Open news modal for adding new news
        function openNewsModal() {
            newsIdInput.value = '';
        newsTitleInput.value = '';
        newsDescriptionInput.value = '';
        newsImageInput.value = '';
        newsImagePreviewContainer.style.display = 'none';
        newsModalTitle.textContent = 'افزودن خبر جدید';

        newsModalOverlay.classList.add('active');
        newsModal.classList.add('active');
        }

        // Edit news
        function editNews(newsId) {
            const newsItem = news.find(n => n.id == newsId);
        if (newsItem) {
            newsIdInput.value = newsItem.id;
        newsTitleInput.value = newsItem.title;
        newsDescriptionInput.value = newsItem.description;
        newsImagePreview.src = newsItem.image;
        newsImagePreviewContainer.style.display = 'block';
        newsModalTitle.textContent = 'ویرایش خبر';

        newsModalOverlay.classList.add('active');
        newsModal.classList.add('active');
            }
        }

        // Save news (add or update)
        function saveNews() {
            const newsData = {
            title: newsTitleInput.value,
        description: newsDescriptionInput.value,
        image: newsImagePreview.src || 'https://images.unsplash.com/photo-1496128858413-b36217c2ce36?ixlib=rb-1.2.1&auto=format&fit=crop&w=500&q=60'
            };

        if (newsIdInput.value) {
                // Update existing news
                const newsId = parseInt(newsIdInput.value);
                const newsIndex = news.findIndex(n => n.id === newsId);
        if (newsIndex !== -1) {
            news[newsIndex] = { ...news[newsIndex], ...newsData };
                }
            } else {
                // Add new news
                const newId = news.length > 0 ? Math.max(...news.map(n => n.id)) + 1 : 1;
        news.push({id: newId, ...newsData });
            }

        renderNewsCards();
        newsModal.classList.remove('active');
        newsModalOverlay.classList.remove('active');
        }

        // Confirm delete
        function confirmDelete(id, type) {
            currentDeleteId = parseInt(id);
        currentDeleteType = type;

        if (type === 'user') {
                const user = users.find(u => u.id == id);
        deleteMessage.textContent = `آیا مطمئن هستید که می‌خواهید کاربر ${user.firstName} ${user.lastName} را حذف کنید؟`;
            } else if (type === 'news') {
                const newsItem = news.find(n => n.id == id);
        deleteMessage.textContent = `آیا مطمئن هستید که می‌خواهید خبر "${newsItem.title}" را حذف کنید؟`;
            }

        deleteModalOverlay.classList.add('active');
        deleteModal.classList.add('active');
        }

        // Delete user
        function deleteUser(userId) {
            users = users.filter(u => u.id !== userId);
        renderUsersTable();
        }

        // Delete news
        function deleteNews(newsId) {
            news = news.filter(n => n.id !== newsId);
        renderNewsCards();
        }