document.getElementById("login-form").onsubmit = function (event) {
  event.preventDefault();
  const username = document.getElementById("username").value;
  const password = document.getElementById("password").value;

  // Thực hiện xác thực tại đây
  // Giả sử xác thực thành công:
  document.getElementById("login-message").textContent =
    "Đăng nhập thành công!";
};

document.getElementById("register-form").onsubmit = function (event) {
  event.preventDefault();
  const username = document.getElementById("username").value;
  const password = document.getElementById("password").value;
  const confirmPassword = document.getElementById("confirm-password").value;

  // Kiểm tra mật khẩu
  if (password !== confirmPassword) {
    document.getElementById("register-message").textContent =
      "Mật khẩu không khớp!";
    return;
  }

  // Thực hiện đăng ký tại đây
  // Giả sử đăng ký thành công:
  document.getElementById("register-message").textContent =
    "Đăng ký thành công!";
};

function calculateTotal() {
  // Lấy giá từ option được chọn
  const fishTypeElement = document.getElementById("fishType");
  const price = parseInt(fishTypeElement.options[fishTypeElement.selectedIndex].dataset.price);

  // Lấy số lượng
  const quantity = parseInt(document.getElementById("quantity").value);

  // Kiểm tra nếu giá hoặc số lượng không hợp lệ
  if (isNaN(price) || isNaN(quantity)) {
      document.getElementById("totalPrice").innerText = "0 VND";
      return;
  }

  // Tính tổng tiền
  const totalPrice = price * quantity;

  // Hiển thị tổng tiền
  document.getElementById("totalPrice").innerText = totalPrice.toLocaleString('vi-VN') + " VND";
}

// Đảm bảo hàm calculateTotal() chạy ngay khi trang được tải
document.addEventListener('DOMContentLoaded', function() {
  calculateTotal();
});
