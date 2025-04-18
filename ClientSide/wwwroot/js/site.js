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