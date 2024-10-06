from flask import Blueprint, jsonify
from models import Product, db

main_routes = Blueprint('main', __name__)

@main_routes.route('/api/products', methods=['GET'])
def get_products():
    products = Product.query.all()
    return jsonify([{'id': p.id, 'name': p.name, 'price': p.price, 'image': p.image} for p in products])
