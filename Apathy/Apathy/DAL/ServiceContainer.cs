namespace Apathy.DAL
{
    public class ServiceContainer
    {
        private UnitOfWork _uow;
        private BudgetService _budgetService;
        private EnvelopeService _envelopeService;
        private TransactionService _transactionService;
        private UserService _userService;

        public ServiceContainer()
        {
            this._uow = new UnitOfWork();
        }

        public BudgetService BudgetService 
        {
            get 
            {
                if (_budgetService == null)
                    _budgetService = new BudgetService(_uow);

                return _budgetService;
            }
        }

        public EnvelopeService EnvelopeService 
        {
            get
            {
                if (_envelopeService == null)
                    _envelopeService = new EnvelopeService(_uow);

                return _envelopeService;
            }
        }

        public TransactionService TransactionService 
        { 
            get
            {
                if (_transactionService == null)
                    _transactionService = new TransactionService(_uow);
                
                return _transactionService;
            }
        }

        public UserService UserService 
        { 
            get
            {
                if (_userService == null)
                    _userService = new UserService(_uow);

                return _userService;
            }
        }
    }
}