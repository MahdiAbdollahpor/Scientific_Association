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


