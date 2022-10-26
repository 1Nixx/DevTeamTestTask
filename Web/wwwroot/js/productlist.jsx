class FilterOption extends React.Component {
    constructor(props) {
        super(props);

        this.state = { options: [] };
        this.onSelectChange = this.onSelectChange.bind(this)
    }

    loadData() {
        var xhr = new XMLHttpRequest();
        xhr.open("get", "api/shop/all", true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            this.setState({ options: data });
        }.bind(this);
        xhr.send();
    }

    componentDidMount() {
        this.loadData();
    }

    onSelectChange(ev) {
        this.props.onChange(ev.target.value)
    }

    render() {
        return (
            <div>
                <select onChange={this.onSelectChange}>
                    <option>Все магазины</option>
                    {
                        this.state.options.map((option) =>
                            <option key={option.id} value={option.id}>{option.name}</option>
                        )
                    }
                </select>
            </div>
        )
    }


}

class Product extends React.Component {
    constructor(props) {
        super(props);

        this.onProductDelete = this.onProductDelete.bind(this)
    }

    onProductDelete() {
        this.props.onProductDelete(this.props.product.productId)
    }

    render() {
        return (
            <div className="h-50 w-75 m-3 text-light rounded-5 ps-3 pe-3 p-2 d-flex justify-content-between align-items-center bg-secondary">
                <div>
                    <span>id: {this.props.product.productId}  </span>
                    <span>  Название: {this.props.product.name}  </span>
                    <span>  Цена: {this.props.product.price}  </span>
                </div>
                <div>
                    <a className="btn btn-success ms-3" href={"product/edit/" + this.props.product.productId}>Редактировать</a>
                    <button className="btn btn-danger ms-3" onClick={this.onProductDelete}>Удалить</button>
                    <a className="btn btn-success ms-3" href={"product/" + this.props.product.productId}>Детали</a>
                </div>
            </div>
        )
    }
}


class ProductList extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            filter: "Все магазины",
            products: []
        };
        this.onFilterChange = this.onFilterChange.bind(this);
        this.onDelete = this.onDelete.bind(this);
    }

    loadData() {
        var xhr = new XMLHttpRequest();
        xhr.open("get", `/product/all?ShopId=${this.state.filter}`, true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            this.setState({ products: data });
        }.bind(this);
        xhr.send();
    }

    componentDidMount() {
        this.loadData();
    }

    componentDidUpdate(prevProps, prevState) {
        if (prevState.filter !== this.state.filter)
            this.loadData();
    }

    onFilterChange(filterId) {
        this.setState({ filter: filterId });
    }

    onDelete(productId) {
        var xhr = new XMLHttpRequest();
        xhr.open("delete", `/product/${productId}`, true);
        xhr.onload = function () {
            this.loadData();
        }.bind(this);
        xhr.send();
    }


    render() {
        return (
            <div className="mt-4">
                <div className="d-flex justify-content-between align-items-center w-75">
                    < FilterOption onChange={this.onFilterChange} />
                    <a className="btn btn-success" href="/product/add">Добавить продукт</a>
                </div>
                <hr />
                <div className="mt-4 d-flex-column">
                    {
                        this.state.products.map((product) =>
                            < Product key={product.productId} product={product} onProductDelete={this.onDelete} />
                        )
                    }
                </div>
            </div>);
    }
}
ReactDOM.render(
    < ProductList />,
    document.getElementById("productsection")
);