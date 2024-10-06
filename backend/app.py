from flask import Flask, jsonify, request
from flask_sqlalchemy import SQLAlchemy
from models import db, User, Product, Order

app = Flask(__name__)
app.config['SQLALCHEMY_DATABASE_URI'] = 'sqlite:///fishfarm.db'
app.config['SQLALCHEMY_TRACK_MODIFICATIONS'] = False
db.init_app(app)

@app.before_first_request
def create_tables():
    db.create_all()  # Tạo bảng khi ứng dụng khởi động

@app.route('/api/products', methods=['GET'])
def get_products():
    products = Product.query.all()
    return jsonify([product.serialize() for product in products])

@app.route('/api/products/<int:product_id>', methods=['GET'])
def get_product(product_id):
    product = Product.query.get_or_404(product_id)
    return jsonify(product.serialize())

@app.route('/api/products', methods=['POST'])
def add_product():
    data = request.get_json()
    new_product = Product(name=data['name'], price=data['price'], description=data['description'])
    db.session.add(new_product)
    db.session.commit()
    return jsonify(new_product.serialize()), 201

@app.route('/api/orders', methods=['POST'])
def add_order():
    data = request.get_json()
    new_order = Order(customer_id=data['customer_id'], product_id=data['product_id'], quantity=data['quantity'])
    db.session.add(new_order)
    db.session.commit()
    return jsonify(new_order.serialize()), 201

@app.route('/api/users', methods=['POST'])
def add_user():
    data = request.get_json()
    new_user = User(username=data['username'], password=data['password'])
    db.session.add(new_user)
    db.session.commit()
    return jsonify(new_user.serialize()), 201

if __name__ == '__main__':
    app.run(debug=True)

from flask import Flask, jsonify, request, session
from flask_sqlalchemy import SQLAlchemy
from werkzeug.security import generate_password_hash, check_password_hash
from models import db, User, Product, Order

app = Flask(__name__)
app.secret_key = 'your_secret_key'  # Thay đổi khóa bí mật này thành một chuỗi ngẫu nhiên
app.config['SQLALCHEMY_DATABASE_URI'] = 'sqlite:///fishfarm.db'
app.config['SQLALCHEMY_TRACK_MODIFICATIONS'] = False
db.init_app(app)

@app.before_first_request
def create_tables():
    db.create_all()

# Route đăng ký
@app.route('/api/register', methods=['POST'])
def register():
    data = request.get_json()
    if User.query.filter_by(username=data['username']).first():
        return jsonify({'message': 'Tên người dùng đã tồn tại!'}), 400
    
    new_user = User(username=data['username'], password=generate_password_hash(data['password']))
    db.session.add(new_user)
    db.session.commit()
    return jsonify(new_user.serialize()), 201

# Route đăng nhập
@app.route('/api/login', methods=['POST'])
def login():
    data = request.get_json()
    user = User.query.filter_by(username=data['username']).first()
    
    if user and check_password_hash(user.password, data['password']):
        session['user_id'] = user.id
        return jsonify({'message': 'Đăng nhập thành công!'}), 200
    return jsonify({'message': 'Tên người dùng hoặc mật khẩu không đúng!'}), 401

# Thêm các route khác ở đây...

if __name__ == '__main__':
    app.run(debug=True)
